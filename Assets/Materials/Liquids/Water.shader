// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Teagher/Liquids/Water"
{
	Properties
	{
		_Distance("Distance", Float) = 1
		_ShallowColor("ShallowColor", Color) = (0,0.777024,1,0.3568628)
		_DeepColor("DeepColor", Color) = (0,0.2661768,0.490566,1)
		_DistortionForce("DistortionForce", Float) = 1
		_DistortionSpeed("DistortionSpeed", Float) = 1
		_DistortionNoiseScale("DistortionNoiseScale", Float) = 10
		_FoamCutoff("FoamCutoff", Float) = 0.5
		_FoamAmount("FoamAmount", Float) = 1
		_FoamNoiseScale("FoamNoiseScale", Float) = 10
		_FoamNoiseSpeed("FoamNoiseSpeed", Float) = 1
		_FoamColor("FoamColor", Color) = (1,1,1,1)
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_SparkleTreshold("SparkleTreshold", Float) = 0.68
		_SparkleIntensity("SparkleIntensity", Float) = 5
		_SparkleSmoothing("SparkleSmoothing", Float) = 0.1
		_ViewAngleSparkleReduction("View Angle Sparkle Reduction", Float) = 0.95
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
			float3 viewDir;
			float3 worldNormal;
		};

		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _DistortionSpeed;
		uniform float _DistortionNoiseScale;
		uniform float _DistortionForce;
		uniform float4 _ShallowColor;
		uniform float4 _DeepColor;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Distance;
		uniform float4 _FoamColor;
		uniform float _FoamAmount;
		uniform float _FoamCutoff;
		uniform float _FoamNoiseSpeed;
		uniform float _FoamNoiseScale;
		uniform float _ViewAngleSparkleReduction;
		uniform float _SparkleTreshold;
		uniform float _SparkleSmoothing;
		uniform float _SparkleIntensity;
		uniform float _Metallic;
		uniform float _Smoothness;


		float2 unity_gradientNoise_dir( float2 p )
		{
			p = p % 289;
			float x = (34 * p.x + 1) * p.x % 289 + p.y;
			x = (34 * x + 1) * x % 289;
			x = frac(x / 41) * 2 - 1;
			return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
		}


		float unity_gradientNoise( float2 p )
		{
			float2 ip = floor(p);
			float2 fp = frac(p);
			float d00 = dot(unity_gradientNoise_dir(ip), fp);
			float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
			float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
			float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
			fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
			return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
		}


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float Unity_GradientNoise_float54_g12( float2 UV , float Scale )
		{
			return unity_gradientNoise(UV * Scale) + 0.5;
		}


		float Unity_GradientNoise_float54_g11( float2 UV , float Scale )
		{
			return unity_gradientNoise(UV * Scale) + 0.5;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float mulTime30 = _Time.y * _DistortionSpeed;
			float simplePerlin2D28 = snoise( ( i.uv_texcoord + mulTime30 )*_DistortionNoiseScale );
			simplePerlin2D28 = simplePerlin2D28*0.5 + 0.5;
			float4 screenColor21 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_screenPosNorm + ( simplePerlin2D28 * _DistortionForce ) ).xy);
			float eyeDepth16 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float4 lerpResult19 = lerp( _ShallowColor , _DeepColor , saturate( ( ( eyeDepth16 - ase_screenPos.w ) / _Distance ) ));
			float4 lerpResult22 = lerp( screenColor21 , lerpResult19 , lerpResult19.a);
			float eyeDepth40 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float mulTime53 = _Time.y * _FoamNoiseSpeed;
			float2 UV54_g12 = ( i.uv_texcoord + mulTime53 );
			float Scale54_g12 = _FoamNoiseScale;
			float localUnity_GradientNoise_float54_g12 = Unity_GradientNoise_float54_g12( UV54_g12 , Scale54_g12 );
			float2 temp_cast_1 = (mulTime53).xx;
			float2 UV54_g11 = ( i.uv_texcoord - temp_cast_1 );
			float Scale54_g11 = _FoamNoiseScale;
			float localUnity_GradientNoise_float54_g11 = Unity_GradientNoise_float54_g11( UV54_g11 , Scale54_g11 );
			float temp_output_84_0 = ( localUnity_GradientNoise_float54_g12 * localUnity_GradientNoise_float54_g11 );
			float4 temp_output_57_0 = ( _FoamColor * step( ( saturate( ( ( eyeDepth40 - ase_screenPos.w ) / _FoamAmount ) ) * _FoamCutoff ) , temp_output_84_0 ) );
			o.Albedo = ( lerpResult22 + temp_output_57_0 ).rgb;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float dotResult125 = dot( reflect( i.viewDir , ase_vertexNormal ) , ase_vertexNormal );
			float smoothstepResult96 = smoothstep( ( _SparkleTreshold - _SparkleSmoothing ) , ( _SparkleTreshold + _SparkleSmoothing ) , temp_output_84_0);
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			o.Emission = ( step( abs( dotResult125 ) , _ViewAngleSparkleReduction ) * ( ( 1.0 - temp_output_57_0 ) * float4( ( ( smoothstepResult96 * _SparkleIntensity ) * ase_lightColor.rgb ) , 0.0 ) ) ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float3 worldNormal : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17400
1920;0;1396;1059;1581.043;987.7064;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;66;-2175.785,394.5352;Inherit;False;1988.892;981.4056;Foam;20;54;40;41;53;51;42;49;52;44;55;50;45;48;58;47;57;69;80;83;84;;0.5471698,0.5471698,0.5471698,1;0;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;41;-2018.001,671.8581;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;40;-1993.301,562.6581;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1766.805,1051.412;Inherit;False;Property;_FoamNoiseSpeed;FoamNoiseSpeed;9;0;Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;64;-1757.555,-916.2452;Inherit;False;1569.357;592.8857;Distortion;11;21;29;32;27;28;33;38;31;39;30;34;;1,0.495283,0.6980782,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-1788.784,913.5179;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;42;-1557.801,643.2582;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;53;-1487.82,1056.475;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-1638.173,780.1472;Inherit;False;Property;_FoamAmount;FoamAmount;7;0;Create;True;0;0;False;0;1;0.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;65;-1667.016,-293.1342;Inherit;False;1473.956;668.486;Color from depth;10;2;16;3;4;6;17;18;5;19;26;;0,0.7899308,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-1672.796,-659.1517;Inherit;False;Property;_DistortionSpeed;DistortionSpeed;4;0;Create;True;0;0;False;0;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1197.688,1066.711;Inherit;False;Property;_FoamNoiseScale;FoamNoiseScale;8;0;Create;True;0;0;False;0;10;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;104;-54.491,805.3465;Inherit;False;1121.13;575.0831;Sparkles;9;103;102;94;93;96;100;99;98;97;;1,0.7270336,0,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-1226.661,918.469;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;83;-1239.437,1192.776;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;44;-1371.9,635.4581;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;2;-1617.016,145.9518;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;30;-1448.068,-658.97;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;16;-1592.316,36.75183;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-1485.559,-794.1709;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;80;-948.9485,1114.016;Inherit;False;GradientNoise;-1;;11;73bcad20642e36b47bcbf1cdbeca1c3f;0;2;2;FLOAT2;0,0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;45;-1215.9,638.058;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-1409.067,-478.9702;Inherit;False;Property;_DistortionNoiseScale;DistortionNoiseScale;5;0;Create;True;0;0;False;0;10;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-1246.796,-715.1517;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-1156.816,117.3519;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-25.58483,977.5654;Inherit;False;Property;_SparkleSmoothing;SparkleSmoothing;15;0;Create;True;0;0;False;0;0.1;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;69;-952.9547,912.6838;Inherit;False;GradientNoise;-1;;12;73bcad20642e36b47bcbf1cdbeca1c3f;0;2;2;FLOAT2;0,0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-1373.599,776.1089;Inherit;False;Property;_FoamCutoff;FoamCutoff;6;0;Create;True;0;0;False;0;0.5;1.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1277.716,260.3519;Inherit;False;Property;_Distance;Distance;0;0;Create;True;0;0;False;0;1;1.58;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;-11.31136,857.4559;Inherit;False;Property;_SparkleTreshold;SparkleTreshold;13;0;Create;True;0;0;False;0;0.68;0.88;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-990.0676,-442.6835;Inherit;False;Property;_DistortionForce;DistortionForce;3;0;Create;True;0;0;False;0;1;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;113;343.6699,162.5353;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;6;-970.9149,109.5518;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;28;-1075.766,-615.1725;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;116;367.4698,372.7352;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-673.8029,970.818;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;191.5089,951.3466;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-999.9572,639.7802;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;100;176.509,860.3465;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ReflectOpNode;114;559.6699,209.9353;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;18;-1024.434,-78.53941;Inherit;False;Property;_DeepColor;DeepColor;2;0;Create;True;0;0;False;0;0,0.2661768,0.490566,1;0,0.2360751,0.4339621,0.9803922;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;5;-814.9148,112.1518;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;27;-868.9661,-813.1725;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;96;386.2507,860.863;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;47;-542.4061,674.7594;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;-1027.451,-243.1342;Inherit;False;Property;_ShallowColor;ShallowColor;1;0;Create;True;0;0;False;0;0,0.777024,1,0.3568628;0.3066036,0.7036073,1,0.627451;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;58;-761.0472,444.5352;Inherit;False;Property;_FoamColor;FoamColor;10;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;333.5677,1085.779;Inherit;False;Property;_SparkleIntensity;SparkleIntensity;14;0;Create;True;0;0;False;0;5;5.81;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-733.0674,-567.97;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-532.666,-573.8724;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DotProductOpNode;125;753.1711,279.4915;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;-636.9046,-31.70849;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;94;583.3214,887.2561;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-355.8913,484.3516;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;102;496.7383,1217.656;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;112;737.1245,582.8917;Inherit;False;447.1889;183;Remove sparkles from foam;2;111;110;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;110;787.1245,635.6069;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.AbsOpNode;126;913.3943,285.3071;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;703.1328,413.5653;Inherit;False;Property;_ViewAngleSparkleReduction;View Angle Sparkle Reduction;16;0;Create;True;0;0;False;0;0.95;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;21;-411.6012,-591.5532;Inherit;False;Global;_GrabScreen0;Grab Screen 0;3;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;26;-464.0594,39.71093;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;863.7711,958.5176;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;22;274.2075,-87.77441;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;1015.313,632.8917;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;128;1074.333,305.3654;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;1374.37,367.0352;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;92;1344.741,125.3627;Inherit;False;Property;_Smoothness;Smoothness;12;0;Create;True;0;0;False;0;0;0.875;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;501.373,-88.78913;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;91;1357.329,44.12743;Inherit;False;Property;_Metallic;Metallic;11;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1755.105,-23.18379;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;42;0;40;0
WireConnection;42;1;41;4
WireConnection;53;0;54;0
WireConnection;52;0;51;0
WireConnection;52;1;53;0
WireConnection;83;0;51;0
WireConnection;83;1;53;0
WireConnection;44;0;42;0
WireConnection;44;1;49;0
WireConnection;30;0;34;0
WireConnection;80;2;83;0
WireConnection;80;3;55;0
WireConnection;45;0;44;0
WireConnection;38;0;39;0
WireConnection;38;1;30;0
WireConnection;3;0;16;0
WireConnection;3;1;2;4
WireConnection;69;2;52;0
WireConnection;69;3;55;0
WireConnection;6;0;3;0
WireConnection;6;1;4;0
WireConnection;28;0;38;0
WireConnection;28;1;31;0
WireConnection;84;0;69;0
WireConnection;84;1;80;0
WireConnection;99;0;97;0
WireConnection;99;1;98;0
WireConnection;48;0;45;0
WireConnection;48;1;50;0
WireConnection;100;0;97;0
WireConnection;100;1;98;0
WireConnection;114;0;113;0
WireConnection;114;1;116;0
WireConnection;5;0;6;0
WireConnection;96;0;84;0
WireConnection;96;1;100;0
WireConnection;96;2;99;0
WireConnection;47;0;48;0
WireConnection;47;1;84;0
WireConnection;32;0;28;0
WireConnection;32;1;33;0
WireConnection;29;0;27;0
WireConnection;29;1;32;0
WireConnection;125;0;114;0
WireConnection;125;1;116;0
WireConnection;19;0;17;0
WireConnection;19;1;18;0
WireConnection;19;2;5;0
WireConnection;94;0;96;0
WireConnection;94;1;93;0
WireConnection;57;0;58;0
WireConnection;57;1;47;0
WireConnection;110;0;57;0
WireConnection;126;0;125;0
WireConnection;21;0;29;0
WireConnection;26;0;19;0
WireConnection;103;0;94;0
WireConnection;103;1;102;1
WireConnection;22;0;21;0
WireConnection;22;1;19;0
WireConnection;22;2;26;3
WireConnection;111;0;110;0
WireConnection;111;1;103;0
WireConnection;128;0;126;0
WireConnection;128;1;129;0
WireConnection;119;0;128;0
WireConnection;119;1;111;0
WireConnection;56;0;22;0
WireConnection;56;1;57;0
WireConnection;0;0;56;0
WireConnection;0;2;119;0
WireConnection;0;3;91;0
WireConnection;0;4;92;0
ASEEND*/
//CHKSM=51DEA3BC9F448DCD90E4B10B69A0C741869FD3A8