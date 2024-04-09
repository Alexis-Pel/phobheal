// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Teagher/Liquids/Noisy"
{
	Properties
	{
		_NoiseTexture("NoiseTexture", 2D) = "white" {}
		_Speed("Speed", Float) = 0.001
		_Scale("Scale", Vector) = (0,0,0,0)
		[HDR]_Color1("Color1", Color) = (0.3018868,0.1444321,0,1)
		[HDR]_Color2("Color2", Color) = (1,0,0,1)
		_Smoothstep("Smoothstep", Vector) = (0,1,0,0)
		_EmissionIntensity("EmissionIntensity", Float) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform float2 _Smoothstep;
		uniform sampler2D _NoiseTexture;
		uniform float2 _Scale;
		uniform float _Speed;
		uniform float _EmissionIntensity;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord3 = i.uv_texcoord * _Scale;
			float mulTime6 = _Time.y * _Speed;
			float2 temp_cast_0 = (mulTime6).xx;
			float temp_output_11_0 = ( tex2D( _NoiseTexture, ( uv_TexCoord3 + mulTime6 ) ).r + tex2D( _NoiseTexture, ( uv_TexCoord3 - temp_cast_0 ) ).r );
			float2 temp_cast_1 = (temp_output_11_0).xx;
			float2 uv_TexCoord14 = i.uv_texcoord + temp_cast_1;
			float2 temp_cast_2 = (mulTime6).xx;
			float smoothstepResult19 = smoothstep( _Smoothstep.x , _Smoothstep.y , ( tex2D( _NoiseTexture, uv_TexCoord14 ).r * temp_output_11_0 ));
			float4 lerpResult21 = lerp( _Color1 , _Color2 , smoothstepResult19);
			o.Albedo = lerpResult21.rgb;
			o.Emission = ( lerpResult21 * _EmissionIntensity ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18908
2157;0;1290;1059;-1328.463;692.0142;1.472353;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-713.3973,242.5841;Inherit;False;Property;_Speed;Speed;1;0;Create;True;0;0;0;False;0;False;0.001;0.001;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;33;-774.4465,23.22946;Inherit;False;Property;_Scale;Scale;2;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-359.2398,72.69068;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;6;-524.4656,247.7217;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-72.49689,108.7667;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;8;-64.69686,282.967;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-781.1397,-753.4272;Inherit;True;Property;_NoiseTexture;NoiseTexture;0;0;Create;True;0;0;0;False;0;False;0cdfab21216771041a13045c183edb87;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;9;363.0028,212.767;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;348.5599,-56.80928;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;11;770.351,107.1932;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;1085.214,-184.157;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;1465.537,-374.7541;Inherit;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;2140.209,-196.6068;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;20;2126.667,123.4366;Inherit;False;Property;_Smoothstep;Smoothstep;5;0;Create;True;0;0;0;False;0;False;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;22;2337.236,-381.3386;Inherit;False;Property;_Color2;Color2;4;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,1;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;2337.236,-575.3386;Inherit;False;Property;_Color1;Color1;3;1;[HDR];Create;True;0;0;0;False;0;False;0.3018868,0.1444321,0,1;0.3018868,0.1444321,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;19;2469.795,-98.54828;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;21;2740.236,-237.3386;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;2706.236,140.6614;Inherit;False;Property;_EmissionIntensity;EmissionIntensity;6;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;3068.236,-68.33856;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;26;2869.236,220.6614;Inherit;False;Property;_Metallic;Metallic;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;2853.236,304.6614;Inherit;False;Property;_Smoothness;Smoothness;8;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3355.218,-182.0908;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Teagher/Liquids/Noisy;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;16;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;33;0
WireConnection;6;0;10;0
WireConnection;7;0;3;0
WireConnection;7;1;6;0
WireConnection;8;0;3;0
WireConnection;8;1;6;0
WireConnection;9;0;1;0
WireConnection;9;1;8;0
WireConnection;2;0;1;0
WireConnection;2;1;7;0
WireConnection;11;0;2;1
WireConnection;11;1;9;1
WireConnection;14;1;11;0
WireConnection;13;0;1;0
WireConnection;13;1;14;0
WireConnection;15;0;13;1
WireConnection;15;1;11;0
WireConnection;19;0;15;0
WireConnection;19;1;20;1
WireConnection;19;2;20;2
WireConnection;21;0;23;0
WireConnection;21;1;22;0
WireConnection;21;2;19;0
WireConnection;24;0;21;0
WireConnection;24;1;25;0
WireConnection;0;0;21;0
WireConnection;0;2;24;0
WireConnection;0;3;26;0
WireConnection;0;4;27;0
ASEEND*/
//CHKSM=12E20A1022998789F0C536DAAFBE4C68EFD1C252