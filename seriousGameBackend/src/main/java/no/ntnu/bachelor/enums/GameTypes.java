package no.ntnu.bachelor.enums;

public enum GameTypes
{
    Standard("StandardQuiz"),
    TrueOrFalse ("SantEllerUsantQuiz"),
    MemoryCards ("MinnekortQuiz"),
    Rank ("SorteringsQuiz");

    private final String value;

    GameTypes(String value) { this.value = value; }

    @Override
    public String toString() { return value; }
}
