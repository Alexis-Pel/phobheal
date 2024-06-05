using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static Vector3 startPosition;

    // Instrucitons
    public static int instructionNextScene = 1;

    public static string baseInstruction = "Merci d'avoir choisi Phobheal, votre solution immersive en réalité virtuelle. \r\n\r\nÀ travers cette application, nous vous proposons une expérience thérapeutique innovante, pour vous aider à surmonter vos peurs de manière sécurisée et progressive.";
    public static string experienceInstruction = "N'oubliez pas que vous avez le contrôle total de l'expérience. À tout moment, vous pouvez faire une pause ou arrêter la session. Votre bien-être est notre priorité.\r\n\r\nNous espérons que Phobheal vous aidera à surmonter vos phobies et à retrouver une vie plus libre et sereine.";
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
