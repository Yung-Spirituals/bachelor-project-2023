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

            Story s1 = new Story("Geografi", "iconUrl", "backgroundUrl",
                    "A plant grown in the dirt", "What is a potato?",
                    "Test geografikunnskapene dine med varierte quizspørsmål fra lett til avansert");
            Story s2 = new Story("Musikk", "iconUrl", "backgroundUrl",
                    "A plant that makes you cry", "what is an onion?",
                    "Test musikkkunnskapene dine med varierte quizspørsmål fra lett til avansert");
            Story s3 = new Story("Film", "iconUrl", "backgroundUrl",
                    "An edible red ball", "What is a tomato?",
                    "Test filmkunnskapene dine med varierte quizspørsmål fra lett til avansert");

            Level s1l1 = new Level(s1, "Nivå 1", "url", GameTypes.Standard.name(),
                    "Trykk på knappen som innholder riktig svar til spørsmålene. Spørsmålene handler om " +
                            "hva hovedstaden til forskjellige land heter.", "");

            Level s1l2 = new Level(s1, "Nivå 2", "url", GameTypes.TrueOrFalse.name(),
                    "Trykk på knappen med sant eller usant basert på utsagnet som blir vist", "");

            Level s1l3 = new Level(s1, "Nivå 3", "url", GameTypes.Rank.name(),
                    "Beat the level", "");


            Level s2l1 = new Level(s2, "Nivå 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s2l2 = new Level(s2, "Nivå 2", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s3l1 = new Level(s3, "Nivå 1", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");
            Level s3l2 = new Level(s3, "Nivå 2", "url", GameTypes.Standard.name(),
                    "Beat the level", "Git gud");


            // Standard.
            Question s1l1q1 = new Question(s1l1, "Hva er hovedstaden i Australia?", "imageUrl",
                    "Sydney", "Canberra", "Melbourne", "Adeleide",
                    false, true, false, false);

            Question s1l1q2 = new Question(s1l1, "Hva er hovedstaden i Canada?", "imageUrl",
                    "Toronto", "Vancouver", "Winnipeg", "Ottawa",
                    false, false, false, true);

            Question s1l1q3 = new Question(s1l1, "Hva er hovedstaden i Hellas?", "imageUrl",
                    "Creta", "Athens", "Heraklion", "Rethimno",
                    false, true, false, false);

            Question s1l1q4 = new Question(s1l1, "Hva er hovedstaden i Japan?", "imageUrl",
                    "Osaka", "Kyoto", "Tokyo", "Yokohama",
                    false, false, true, false);

            Question s1l1q5 = new Question(s1l1, "Hva er hovedstaden i India?", "imageUrl",
                    "Mumbai", "New Delhi", "Chennai", "Kolkata",
                    false, true, false, false);

            Question s1l1q6 = new Question(s1l1, "Hva er hovedstaden i Island?", "imageUrl",
                    "Reykjavik", "Akureyri", "Mosfellsbær", "Kópavogur",
                    true, false, false, false);

            Question s1l1q7 = new Question(s1l1, "Hva er hovedstaden i Cuba", "imageUrl",
                    "Varadero", "Cienfuegos", "Santiago de Cuba", "Havana",
                    false, false, false, true);

            Question s1l1q8 = new Question(s1l1, "Hva er hovedstaden i El Salvador?", "imageUrl",
                    "Apopa", "Delgado", "San Salvador", "Santa Tecla",
                    false, false, true, false);

            Question s1l1q9 = new Question(s1l1, "Hva er hovedstaden i Indonesia?", "imageUrl",
                    "Jakarta", "Medan", "Semarang", "Bandung",
                    true, false, false, false);

            Question s1l1q10 = new Question(s1l1, "Hva er hovedstaden i Haiti?", "imageUrl",
                    "Port-au-Prince", "Delmas", "Carrefour", "Les Cayes",
                    true, false, false, false);


            // True or false.
            Question s1l2q1 = new Question(s1l2, "Norge er landet med lengst kystlinje", "imageUrl",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q2 = new Question(s1l2, "Island er landet med de mest aktive vulkanene i verden", "imageUrl",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q3 = new Question(s1l2, "Kina grenser til 14 andre land", "imageUrl",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q4 = new Question(s1l2, "Eiffeltårnet er lokalt kjent som \"La Dame de Fer\"", "imageUrl",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q5 = new Question(s1l2, "Nepal er et innlandsstat", "imageUrl",
                    "Sant", "Usant", "", "",
                    true, false, false, false);


            // Rank.
            Question s1l3q1 = new Question(s1l3, "Sorter landene fra størst til minst i størrelse", "imageUrl",
                    "Russland", "USA", "Australia", "India",
                    true, false, false, false);

            Question s1l3q2 = new Question(s1l3, "Sorter de nordiske landene fra størst til minst i størrelse", "imageUrl",
                    "Sverige", "Finland", "Norge", "Danmark",
                    true, false, false, false);

            Question s1l3q3 = new Question(s1l3, "Sorter landene fra største populasjon til minste populasjon i størrelse", "imageUrl",
                    "USA", "Brazil", "Philippines", "Egypt",
                    true, false, false, false);


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
            levelRepository.save(s1l3);

            levelRepository.save(s2l1);
            levelRepository.save(s2l2);
            levelRepository.save(s3l1);
            levelRepository.save(s3l2);

            questionRepository.save(s1l1q1);
            questionRepository.save(s1l1q2);
            questionRepository.save(s1l1q3);
            questionRepository.save(s1l1q4);
            questionRepository.save(s1l1q5);
            questionRepository.save(s1l1q6);
            questionRepository.save(s1l1q7);
            questionRepository.save(s1l1q8);
            questionRepository.save(s1l1q9);
            questionRepository.save(s1l1q10);


            questionRepository.save(s1l2q1);
            questionRepository.save(s1l2q2);
            questionRepository.save(s1l2q3);
            questionRepository.save(s1l2q4);
            questionRepository.save(s1l2q5);


            questionRepository.save(s1l3q1);
            questionRepository.save(s1l3q2);
            questionRepository.save(s1l3q3);

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