package no.ntnu.bachelor;

import java.util.Optional;

import no.ntnu.bachelor.enums.GameTypes;
import no.ntnu.bachelor.models.Level;
import no.ntnu.bachelor.models.Question;
import no.ntnu.bachelor.models.Story;
import no.ntnu.bachelor.repositories.LevelRepository;
import no.ntnu.bachelor.repositories.QuestionRepository;
import no.ntnu.bachelor.repositories.StoryRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.ApplicationListener;
import org.springframework.stereotype.Component;

@Component
public class TestDataInitializer implements ApplicationListener<ApplicationReadyEvent> {

    @Autowired
    private StoryRepository storyRepository;

    @Autowired
    private LevelRepository levelRepository;

    @Autowired
    private QuestionRepository questionRepository;

    private final Logger logger = LoggerFactory.getLogger("TestInitializer");

    @Override
    public void onApplicationEvent(ApplicationReadyEvent event) {
        Optional<Story> existingStory = Optional.ofNullable(storyRepository.findBy_storyName("potato"));
        if (existingStory.isEmpty()) {
            logger.info("Importing test data..." +
                    GameTypes.Standard.name());

            Story s1 = new Story("potato", "iconUrl", "backgroundUrl",
                    "A plant grown in the dirt", "What is a potato?",
                    "A potato is a common food, Ireland in particular is famous for their relationship" +
                            " with potatoes as it proved essential to the survival" +
                            " of many Irishmen during times of famine.");
            Story s2 = new Story("onion", "iconUrl", "backgroundUrl",
                    "A plant that makes you cry", "what is an onion?",
                    "Cutting an onion often leaves you crying as they attack with a vicious tear gas.");
            Story s3 = new Story("tomato", "iconUrl", "backgroundUrl",
                    "An edible red ball", "What is a tomato?",
                    "Tomatoes are red and round.");

            Level s1l1 = new Level(s1, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s1l2 = new Level(s1, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s2l1 = new Level(s2, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s2l2 = new Level(s2, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s3l1 = new Level(s3, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s3l2 = new Level(s3, "level 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");

            Question s1l1q1 = new Question(s1l1, "Question s1l1q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s1l1q2 = new Question(s1l1, "Question s1l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s1l1q3 = new Question(s1l1, "Question s1l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s1l2q1 = new Question(s1l2, "Question s1l2q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s1l2q2 = new Question(s1l2, "Question s1l2q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s1l2q3 = new Question(s1l2, "Question s1l2q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l1q1 = new Question(s2l1, "Question s2l1q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l1q2 = new Question(s2l1, "Question s2l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l1q3 = new Question(s2l1, "Question s2l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q1 = new Question(s2l2, "Question s2l2q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q2 = new Question(s2l2, "Question s2l2q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q3 = new Question(s2l2, "Question s2l2q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q1 = new Question(s3l1, "Question s3l1q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q2 = new Question(s3l1, "Question s3l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q3 = new Question(s3l1, "Question s3l1q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q1 = new Question(s3l2, "Question s3l2q1", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q2 = new Question(s3l2, "Question s3l2q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q3 = new Question(s3l2, "Question s3l2q2", "imageUrl",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);

            storyRepository.save(s1);
            storyRepository.save(s2);
            storyRepository.save(s3);

            levelRepository.save(s1l1);
            levelRepository.save(s1l2);
            levelRepository.save(s2l1);
            levelRepository.save(s2l2);
            levelRepository.save(s3l1);
            levelRepository.save(s3l2);

            questionRepository.save(s1l1q1);
            questionRepository.save(s1l1q2);
            questionRepository.save(s1l1q3);
            questionRepository.save(s1l2q1);
            questionRepository.save(s1l2q2);
            questionRepository.save(s1l2q3);
            questionRepository.save(s2l1q1);
            questionRepository.save(s2l1q2);
            questionRepository.save(s2l1q3);
            questionRepository.save(s2l2q1);
            questionRepository.save(s2l2q2);
            questionRepository.save(s2l2q3);
            questionRepository.save(s3l1q1);
            questionRepository.save(s3l1q2);
            questionRepository.save(s3l1q3);
            questionRepository.save(s3l2q1);
            questionRepository.save(s3l2q2);
            questionRepository.save(s3l2q3);

            logger.info("Finished importing test data");
        } else {
            logger.info("User already exists, importing stopped");
        }
    }
}