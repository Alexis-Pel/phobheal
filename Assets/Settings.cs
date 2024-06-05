using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static Vector3 startPosition;

    // Instrucitons
    public static int instructionNextScene = 1;

    public static string baseInstruction = "Merci d'avoir choisi Phobheal, votre solution immersive en r�alit� virtuelle. \r\n\r\n� travers cette application, nous vous proposons une exp�rience th�rapeutique innovante, pour vous aider � surmonter vos peurs de mani�re s�curis�e et progressive.";
    public static string experienceInstruction = "N'oubliez pas que vous avez le contr�le total de l'exp�rience. � tout moment, vous pouvez faire une pause ou arr�ter la session. Votre bien-�tre est notre priorit�.\r\n\r\nNous esp�rons que Phobheal vous aidera � surmonter vos phobies et � retrouver une vie plus libre et sereine.";
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
