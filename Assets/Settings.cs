using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static Vector3 startPosition;

    // Instrucitons
    public static int instructionNextScene = 1;

    public static string baseInstruction = "Bonjour et bienvenue sur Phobheal \n Ceci est une";
    public static string experienceInstruction = "";
}

public enum ScenesEnum
{
    INSTRUCTION,
    MENU,
    ACROPHOBIA,
    THALASSOPHOBIA,
    KENOPHOBIA,
    AGORAPHOBIA,
    CLAUSTROPHOBIA,
}
