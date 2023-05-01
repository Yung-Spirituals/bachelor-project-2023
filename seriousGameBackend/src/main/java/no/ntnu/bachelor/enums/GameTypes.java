package no.ntnu.bachelor.enums;

public enum GameTypes
{
    Standard("QuizStandard"),
    TrueOrFalse ("QuizTrueOrFalse"),
    DragToSlot ("QuizDragToSlot"),
    MemoryCards ("QuizMemoryCards"),
    Rank ("RankStatements"),
    CardGame ("MinigameCards");

    GameTypes(String gameType) {}
}
