package no.ntnu.bachelor.models;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;


@Entity(name = "questions")
public class Question
{
    @Id
    @GeneratedValue
    private Long id;

    @JsonIgnore
    @ManyToOne
    @JoinColumn(name = "level_id")
    private Level _level;

    private String _question;
    private String _imageUrl;

    private String _option0;
    private String _option1;
    private String _option2;
    private String _option3;

    private boolean _isOption0;
    private boolean _isOption1;
    private boolean _isOption2;
    private boolean _isOption3;

    public Question() {}

    public Question(Level level, String question, String imageUrl,
                    String option0, String option1, String option2, String option3,
                    boolean correctOption0, boolean correctOption1, boolean correctOption2, boolean correctOption3)
    {
        _level = level;
        _question = question;
        _imageUrl = imageUrl;

        _option0 = option0;
        _option1 = option1;
        _option2 = option2;
        _option3 = option3;

        _isOption0 = correctOption0;
        _isOption1 = correctOption1;
        _isOption2 = correctOption2;
        _isOption3 = correctOption3;
    }

    @Override
    public String toString()
    {
        return String.format("Question[id=%d, levelName='%s', question='%s', imageUrl='%s', " +
                        "option0='%s', option1='%s', option2='%s', option3='%s', " +
                        "isOption0='%s', isOption1='%s', isOption2='%s', isOption3='%s']",
                id, _level.get_levelName(), _question, _imageUrl,
                _option0, _option1, _option2, _option3,
                _isOption0, _isOption1, _isOption2, _isOption3);
    }

    public Long getId() { return id; }

    public Level get_level() { return _level; }

    public void set_level(Level level) { _level = level; }

    public String get_question() { return _question; }

    public void set_question(String question) { _question = question; }

    public String get_imageUrl() { return _imageUrl; }

    public void set_imageUrl(String imageUrl) { _imageUrl = imageUrl; }

    public String get_option0() { return _option0; }

    public void set_option0(String option0) { _option0 = option0; }

    public String get_option1() { return _option1; }

    public void set_option1(String option1) { _option1 = option1; }

    public String get_option2() { return _option2; }

    public void set_option2(String option2) { _option2 = option2; }

    public String get_option3() { return _option3; }

    public void set_option3(String option3) { _option3 = option3; }

    //public String[] get_options() { return new String[] {_option0, _option1, _option2, _option3}; }

    public boolean get_isOption0() { return _isOption0; }

    public void set_isOption0(boolean isOption0) { _isOption0 = isOption0; }

    public boolean get_isOption1() { return _isOption1; }

    public void set_isOption1(boolean isOption1) { _isOption1 = isOption1; }

    public boolean get_isOption2() { return _isOption2; }

    public void set_isOption2(boolean isOption2) { _isOption2 = isOption2; }

    public boolean get_isOption3() { return _isOption3; }

    public void set_isOption3(boolean isOption3) { _isOption3 = isOption3; }

    //public boolean[] get_isOptions() { return new boolean [] {_isOption0, _isOption1, _isOption2, _isOption3}; }
}
