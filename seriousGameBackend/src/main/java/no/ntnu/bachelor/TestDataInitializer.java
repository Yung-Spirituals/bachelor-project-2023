package no.ntnu.bachelor;

import java.util.Optional;

import no.ntnu.bachelor.enums.GameTypes;
import no.ntnu.bachelor.models.Level;
import no.ntnu.bachelor.models.Question;
import no.ntnu.bachelor.models.Subject;
import no.ntnu.bachelor.repositories.LevelRepository;
import no.ntnu.bachelor.repositories.QuestionRepository;
import no.ntnu.bachelor.repositories.SubjectRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.ApplicationListener;
import org.springframework.stereotype.Component;

@Component
public class TestDataInitializer implements ApplicationListener<ApplicationReadyEvent>
{
    @Autowired private SubjectRepository subjectRepository;
    @Autowired private LevelRepository levelRepository;
    @Autowired private QuestionRepository questionRepository;

    private final Logger logger = LoggerFactory.getLogger("TestInitializer");

    @Override
    public void onApplicationEvent(ApplicationReadyEvent event) {
        Optional<Subject> existingStory = Optional.ofNullable(subjectRepository.findBySubjectName("Geografi"));
        if (existingStory.isEmpty()) {
            logger.info("Importing test data...");

            Subject s1 = new Subject("Geografi", "backgroundUrl",
                    "Test geografikunnskapene dine med varierte quizspørsmål fra lett til avansert." +
                            " Du må få minst én stjerne på hvert nivå for å komme deg videre.");
            Subject s2 = new Subject("Musikk", "backgroundUrl",
                    "Test musikkkunnskapene dine med varierte quizspørsmål fra lett til avansert." +
                            " Du må få minst én stjerne på hvert nivå for å komme deg videre.");
            Subject s3 = new Subject("Film", "backgroundUrl",
                    "Test filmkunnskapene dine med varierte quizspørsmål fra lett til avansert");

            Level s1l1 = new Level(s1, "Nivå 1", GameTypes.Standard.toString(),
                    "Trykk på knappen som innholder riktig svar til spørsmålene. Spørsmålene handler om " +
                            "hva hovedstaden til forskjellige land heter.");

            Level s1l2 = new Level(s1, "Nivå 2", GameTypes.TrueOrFalse.toString(),
                    "Trykk på knappen med sant eller usant basert på utsagnet som blir vist.");

            Level s1l3 = new Level(s1, "Nivå 3", GameTypes.Rank.toString(),
                    "Sorter panelene etter hva spørsmålene ber om.");

            Level s1l4 = new Level(s1, "Nivå 4", GameTypes.MemoryCards.toString(),
                    "Trykk på kortene for å snu dem om. Match kortene som hører sammen basert på innholdet.");


            Level s2l1 = new Level(s2, "Nivå 1", GameTypes.Standard.toString(), "Beat the level");
            Level s2l2 = new Level(s2, "Nivå 2", GameTypes.Standard.toString(), "Beat the level");
            Level s3l1 = new Level(s3, "Nivå 1", GameTypes.Standard.toString(), "Beat the level");
            Level s3l2 = new Level(s3, "Nivå 2", GameTypes.Standard.toString(), "Beat the level");


            // Standard.
            Question s1l1q1 = new Question(s1l1, "Hva er hovedstaden i Australia?",
                    "https://lp-cms-production.imgix.net/2019-06/f22a1e4f3307b7455a76bffcc0e504b7-canberra.jpg",
                    "Sydney", "Canberra", "Melbourne", "Adeleide",
                    false, true, false, false);

            Question s1l1q2 = new Question(s1l1, "Hva er hovedstaden i Canada?",
                    "https://a.cdn-hotels.com/gdcs/production163/d857/cc29dd0e-f745-4507-80e1-6ae5a3532338.jpg",
                    "Toronto", "Vancouver", "Winnipeg", "Ottawa",
                    false, false, false, true);

            Question s1l1q3 = new Question(s1l1, "Hva er hovedstaden i Hellas?",
                    "https://cdn.britannica.com/66/102266-050-FBDEFCA1/acropolis-city-state-Greece-Athens.jpg",
                    "Creta", "Athen", "Heraklion", "Rethimno",
                    false, true, false, false);

            Question s1l1q4 = new Question(s1l1, "Hva er hovedstaden i Japan?",
                    "https://assets.editorial.aetnd.com/uploads/2013/07/gettyimages-1390815938.jpg",
                    "Osaka", "Kyoto", "Tokyo", "Yokohama",
                    false, false, true, false);

            Question s1l1q5 = new Question(s1l1, "Hva er hovedstaden i India?",
                    "https://www.travelandleisure.com/thmb/iAIrOVW7yWrDG8pZBpKMOvEGuNQ=/1500x0/filters:" +
                            "no_upscale():max_bytes(150000):strip_icc()/" +
                            "new-delhi-india-NEWDELHITG0721-60d592e1603349298a0206d67d08582b.jpg",
                    "Mumbai", "New Delhi", "Chennai", "Kolkata",
                    false, true, false, false);

            Question s1l1q6 = new Question(s1l1, "Hva er hovedstaden i Island?",
                    "https://www.icelandtravel.is/_next/image/?url=https%3A%2F%2Ficetprodgreen.wpengine.com" +
                            "%2Fwp-content%2Fuploads%2F2019%2F03%2FReykjavik.jpg&w=3840&q=75",
                    "Reykjavik", "Akureyri", "Mosfellsbær", "Kópavogur",
                    true, false, false, false);

            Question s1l1q7 = new Question(s1l1, "Hva er hovedstaden i Cuba",
                    "https://content.r9cdn.net/rimg/dimg/a3/1d/05f2b4a1-city-11020-1700c4dba73.jpg" +
                            "?crop=true&width=1020&height=498",
                    "Varadero", "Cienfuegos", "Santiago de Cuba", "Havana",
                    false, false, false, true);

            Question s1l1q8 = new Question(s1l1, "Hva er hovedstaden i El Salvador?",
                    "https://www.thegef.org/sites/default/files/styles/full_width_card/public/" +
                            "shutterstock_578720257_san_salvador.jpg?h=e85f6c07&itok=34HoStFm",
                    "Apopa", "Delgado", "San Salvador", "Santa Tecla",
                    false, false, true, false);

            Question s1l1q9 = new Question(s1l1, "Hva er hovedstaden i Indonesia?",
                    "https://content.r9cdn.net/rimg/dimg/1b/61/ad879e55-city-22380-17ea1cf2107.jpg" +
                            "?crop=true&width=1020&height=498",
                    "Jakarta", "Medan", "Semarang", "Bandung",
                    true, false, false, false);

            Question s1l1q10 = new Question(s1l1, "Hva er hovedstaden i Haiti?",
                    "https://upload.wikimedia.org/wikipedia/commons/3/3a/US_Navy_100221-N-5787K-002_" +
                            "An_aerial_view_of_the_logistical_area_near_the_port_in_Port-au-Prince_%28cropped%29.jpg",
                    "Port-au-Prince", "Delmas", "Carrefour", "Les Cayes",
                    true, false, false, false);


            // True or false.
            Question s1l2q1 = new Question(s1l2, "Frihetsgudinnen står på Ellis øya",
                    "https://www.nps.gov/stli/planyourvisit/images/Liberty-statue-with-manhattan_1.jpg",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q2 = new Question(s1l2, "Taj Mahal ligger i New Delhi",
                    "https://cdn.britannica.com/86/170586-050-AB7FEFAE/Taj-Mahal-Agra-India.jpg",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q3 = new Question(s1l2, "Burj Khalifa ligger i Dubai",
                    "https://media.cnn.com/api/v1/images/stellar/prod/180301130119-burj-khalifa-dubai" +
                            "-guide-8.jpg?q=w_3000,h_2000,x_0,y_1,c_crop",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q4 = new Question(s1l2, "Akropolis ligger i Athen",
                    "https://cdn-imgix.headout.com/microbrands-banner-image/image/" +
                            "b698f96a3bf7e35418940973f33c4708-The%20Acropolis%20of%20Athens.jpeg",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q5 = new Question(s1l2, "Påskeøya ligger utenfor kysten til Chile",
                    "https://media.snl.no/media/45540/standard_compressed_paaskeoeya.jpg",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q6 = new Question(s1l2, "Den Kinesiske Mur beffiner seg kun i Beijing",
                    "https://images.nationalgeographic.org/image/upload/t_edhub_resource_key_image/" +
                            "v1638892506/EducationHub/photos/the-great-wall-of-china.jpg",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q7 = new Question(s1l2, "Mount Rushmore ligger i North Dakota",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Mount_Rushmore_detail_view" +
                            "_%28100MP%29.jpg/1200px-Mount_Rushmore_detail_view_%28100MP%29.jpg",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q8 = new Question(s1l2, "Stonehenge ligger i Amesbury",
                    "https://www.britishmuseum.org/sites/default/files/styles/uncropped_small/public/" +
                            "2021-12/Stonehenge_Courtesy_of_English_Heritage.jpg?itok=0F9_g3yY",
                    "Sant", "Usant", "", "",
                    false, true, false, false);

            Question s1l2q9 = new Question(s1l2, "Victoria Falls ligger mellom Zimbabwe og Zambia",
                    "https://cdn.tourradar.com/s3/content-pages/375/1200x630/EHIBb9.jpeg",
                    "Sant", "Usant", "", "",
                    true, false, false, false);

            Question s1l2q10 = new Question(s1l2, "Golden Gate Bridge forbinder San Francisco og Marin County",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/GoldenGateBridge-001.jpg/" +
                            "1200px-GoldenGateBridge-001.jpg",
                    "Sant", "Usant", "", "",
                    true, false, false, false);


            // Rank.
            Question s1l3q1 = new Question(s1l3, "Sorter landene fra største areal til minste", "",
                    "Russland", "USA", "Australia", "India",
                    true, false, false, false);

            Question s1l3q2 = new Question(s1l3, "Sorter de nordiske landene fra største areal til minste",
                    "", "Sverige", "Finland", "Norge", "Danmark",
                    true, false, false, false);

            Question s1l3q3 = new Question(s1l3, "Sorter USAs stater fra største areal til minste",
                    "", "Alaska", "Texas", "Nevada", "Utah",
                    true, false, false, false);

            Question s1l3q4 = new Question(s1l3, "Sorter verdenskontinentene fra største areal til minste",
                    "", "Asia", "Sør-Amerika", "Antarktis", "Europa",
                    true, false, false, false);

            Question s1l3q5 = new Question(s1l3, "Sorter disse landene i Europa fra største i areal til minste",
                    "", "Sverige", "Tyskland", "Island", "Croatia",
                    true, false, false, false);



            //Card
            Question s1l4q1 = new Question(s1l4, "", "",
                    "Høyeste fjell i Norge?", "Galdhøpiggen", "", "",
                    false, true, false, false);

            Question s1l4q2 = new Question(s1l4, "", "",
                    "Lengste elv i Norge?", "Glomma", "", "",
                    false, true, false, false);

            Question s1l4q3 = new Question(s1l4, "", "",
                    "Lengste fjord i Norge?", "Sognefjorden", "", "",
                    false, true, false, false);

            Question s1l4q4 = new Question(s1l4, "", "",
                    "Største isbre i Norge?", "Jostedalsbreen", "", "",
                    false, true, false, false);

            Question s1l4q5 = new Question(s1l4, "", "",
                    "Dypeste innsjø i Norge?", "Hornindalsvatnet", "", "",
                    false, true, false, false);

            Question s1l4q6 = new Question(s1l4, "", "",
                    "Største innsjø i Norge", "Mjøsa", "", "",
                    false, true, false, false);







            Question s2l1q1 = new Question(s2l1, "Question s2l1q1", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l1q2 = new Question(s2l1, "Question s2l1q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l1q3 = new Question(s2l1, "Question s2l1q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q1 = new Question(s2l2, "Question s2l2q1", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q2 = new Question(s2l2, "Question s2l2q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s2l2q3 = new Question(s2l2, "Question s2l2q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q1 = new Question(s3l1, "Question s3l1q1", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q2 = new Question(s3l1, "Question s3l1q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l1q3 = new Question(s3l1, "Question s3l1q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q1 = new Question(s3l2, "Question s3l2q1", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q2 = new Question(s3l2, "Question s3l2q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);
            Question s3l2q3 = new Question(s3l2, "Question s3l2q2", "",
                    "option0", "option1", "option2", "option3",
                    true, false, false, true);

            subjectRepository.save(s1);
            subjectRepository.save(s2);
            subjectRepository.save(s3);


            levelRepository.save(s1l1);
            levelRepository.save(s1l2);
            levelRepository.save(s1l3);
            levelRepository.save(s1l4);

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
            questionRepository.save(s1l2q6);
            questionRepository.save(s1l2q7);
            questionRepository.save(s1l2q8);
            questionRepository.save(s1l2q9);
            questionRepository.save(s1l2q10);

            questionRepository.save(s1l3q1);
            questionRepository.save(s1l3q2);
            questionRepository.save(s1l3q3);
            questionRepository.save(s1l3q4);
            questionRepository.save(s1l3q5);

            questionRepository.save(s1l4q1);
            questionRepository.save(s1l4q2);
            questionRepository.save(s1l4q3);
            questionRepository.save(s1l4q4);
            questionRepository.save(s1l4q5);
            questionRepository.save(s1l4q6);

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
            logger.info("subject already exists, importing stopped");
        }
    }
}