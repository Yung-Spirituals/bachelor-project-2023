package no.ntnu.bachelor.models;

public class JsonMultiObject
{
    private Story _story;
    private Level _level;
    private Question _question;

    public Story get_story() { return _story; }
    public void set_story(Story story) { _story = story; }
    public Level get_level() { return _level; }
    public void set_level(Level level) { _level = level; }
    public Question get_question() { return _question; }
    public void set_question(Question question) { _question = question; }
}
