package no.ntnu.bachelor.enums;

public enum GameTypes
{
    Standard("QuizStandard"),
    TrueOrFalse ("QuizTrueOrFalse"),
    DragToSlot ("QuizDragToSlot"),
    MemoryCards ("QuizMemoryCards"),
    Rank ("RankStatements"),
    CardGame ("MinigameCards");

    private final String value;

    GameTypes(String value) { this.value = value; }

    @Override
    public String toString() { return value; }
}
