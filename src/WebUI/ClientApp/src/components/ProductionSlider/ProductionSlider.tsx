import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import useAxiosApi from "@/hooks/useAxiosApi";
import { useGetProductionsQuery } from "@/api/queries/production.query";

const ProductionSlider = () => {
    const axiosApi = useAxiosApi();

    // const {
    //     isLoading,
    //     mutateAsync,
    // } = useLoginMutation({
    //     onSuccess: (response: LoginResponse) => {
    //         toast.success("Sucessfully logged in", { theme: 'colored' });
    //         globalLogInDispatch(response);
    //     },
    //     onError: (error: ServerError) => {
    //         toast.error(formatServerError(error), { theme: 'colored' });
    //     }
    // }, axiosApi);

    // const users = useGetProductionsQuery({
    //     page: 1,
    //     size: 10,
    // });

    const productions = [
        {
            "productionId": 48687,
            "tmdbId": "862",
            "imdbId": "tt0114709",
            "title": "Toy Story",
            "overview": "Led by Woody, Andy's toys live happily in his room until Andy's birthday brings Buzz Lightyear onto the scene. Afraid of losing his place in Andy's heart, Woody plots against Buzz. But when circumstances separate Buzz and Woody from their owner, the duo eventually learns to put aside their differences.",
            "director": "John Lasseter",
            "keywords": "jealousy toy boy friendship friends rivalry boy next door new toy toy comes to life",
            "releaseDate": "1995-10-30T00:00:00",
            "genres": [
                "Action",
                "Adventure"
            ],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48688,
            "tmdbId": "8844",
            "imdbId": "tt0113497",
            "title": "Jumanji",
            "overview": "When siblings Judy and Peter discover an enchanted board game that opens the door to a magical world, they unwittingly invite Alan -- an adult who's been trapped inside the game for 26 years -- into their living room. Alan's only hope for freedom is to finish the game, which proves risky as all three find themselves running from giant rhinoceroses, evil monkeys and other terrifying creatures.",
            "director": "Joe Johnston",
            "keywords": "board game disappearance based on children's book new home recluse giant insect",
            "releaseDate": "1995-12-15T00:00:00",
            "genres": [
                "Action",
                "Comedy"
            ],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48689,
            "tmdbId": "15602",
            "imdbId": "tt0113228",
            "title": "Grumpier Old Men",
            "overview": "A family wedding reignites the ancient feud between next-door neighbors and fishing buddies John and Max. Meanwhile, a sultry Italian divorcée opens a restaurant at the local bait shop, alarming the locals who worry she'll scare the fish away. But she's less interested in seafood than she is in cooking up a hot time with Max.",
            "director": "Howard Deutch",
            "keywords": "fishing best friend duringcreditsstinger old men",
            "releaseDate": "1995-12-22T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48690,
            "tmdbId": "31357",
            "imdbId": "tt0114885",
            "title": "Waiting to Exhale",
            "overview": "Cheated on, mistreated and stepped on, the women are holding their breath, waiting for the elusive \"good man\" to break a string of less-than-stellar lovers. Friends and confidants Vannah, Bernie, Glo and Robin talk it all out, determined to find a better way to breathe.",
            "director": "Forest Whitaker",
            "keywords": "based on novel interracial relationship single mother divorce chick flick",
            "releaseDate": "1995-12-22T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48691,
            "tmdbId": "11862",
            "imdbId": "tt0113041",
            "title": "Father of the Bride Part II",
            "overview": "Just when George Banks has recovered from his daughter's wedding, he receives the news that she's pregnant ... and that George's wife, Nina, is expecting too. He was planning on selling their home, but that's a plan that -- like George -- will have to change with the arrival of both a grandchild and a kid of his own.",
            "director": "Charles Shyer",
            "keywords": "baby midlife crisis confidence aging daughter mother daughter relationship pregnancy contraception gynecologist",
            "releaseDate": "1995-02-10T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48692,
            "tmdbId": "949",
            "imdbId": "tt0113277",
            "title": "Heat",
            "overview": "Obsessive master thief, Neil McCauley leads a top-notch crew on various insane heists throughout Los Angeles while a mentally unstable detective, Vincent Hanna pursues him without rest. Each man recognizes and respects the ability and the dedication of the other even though they are aware their cat-and-mouse game may end in violence.",
            "director": "Michael Mann",
            "keywords": "robbery detective bank obsession chase shooting thief honor murder suspense heist betrayal money gang cat and mouse criminal mastermind cult film ex-con heist movie one last job loner bank job neo-noir gun fight crime epic",
            "releaseDate": "1995-12-15T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48693,
            "tmdbId": "11860",
            "imdbId": "tt0114319",
            "title": "Sabrina",
            "overview": "An ugly duckling having undergone a remarkable change, still harbors feelings for her crush: a carefree playboy, but not before his business-focused brother has something to say about it.",
            "director": "Sydney Pollack",
            "keywords": "paris brother brother relationship chauffeur long island fusion millionaire",
            "releaseDate": "1995-12-15T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48694,
            "tmdbId": "9091",
            "imdbId": "tt0114576",
            "title": "Sudden Death",
            "overview": "International action superstar Jean Claude Van Damme teams with Powers Boothe in a Tension-packed, suspense thriller, set against the back-drop of a Stanley Cup game.Van Damme portrays a father whose daughter is suddenly taken during a championship hockey game. With the captors demanding a billion dollars by game's end, Van Damme frantically sets a plan in motion to rescue his daughter and abort an impending explosion before the final buzzer...",
            "director": "Peter Hyams",
            "keywords": "terrorist hostage explosive vice president",
            "releaseDate": "1995-12-22T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48694,
            "tmdbId": "9091",
            "imdbId": "tt0114576",
            "title": "Sudden Death",
            "overview": "International action superstar Jean Claude Van Damme teams with Powers Boothe in a Tension-packed, suspense thriller, set against the back-drop of a Stanley Cup game.Van Damme portrays a father whose daughter is suddenly taken during a championship hockey game. With the captors demanding a billion dollars by game's end, Van Damme frantically sets a plan in motion to rescue his daughter and abort an impending explosion before the final buzzer...",
            "director": "Peter Hyams",
            "keywords": "terrorist hostage explosive vice president",
            "releaseDate": "1995-12-22T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
        {
            "productionId": 48694,
            "tmdbId": "9091",
            "imdbId": "tt0114576",
            "title": "Sudden Death",
            "overview": "International action superstar Jean Claude Van Damme teams with Powers Boothe in a Tension-packed, suspense thriller, set against the back-drop of a Stanley Cup game.Van Damme portrays a father whose daughter is suddenly taken during a championship hockey game. With the captors demanding a billion dollars by game's end, Van Damme frantically sets a plan in motion to rescue his daughter and abort an impending explosion before the final buzzer...",
            "director": "Peter Hyams",
            "keywords": "terrorist hostage explosive vice president",
            "releaseDate": "1995-12-22T00:00:00",
            "genres": [],
            poster: "https://image.tmdb.org/t/p/w500/vgpXmVaVyUL7GGiDeiK1mKEKzcX.jpg"
        },
    ];


    const settings = {
        className: "slider variable-width",
        infinite: false,
        slidesToShow: 5,
        slidesToScroll: 5,
        variableWidth: true,
        initialSlide: 0,
        responsive: [
            {
                breakpoint: 1260,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                }
            },
            {
                breakpoint: 1000,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2,
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    };
    return (
        <div className="mx-auto ">
            <div className="prose prose-slate lg:prose-lg text-white mb-5">
                <h3 className="text-white ">New Trending Movies</h3>
            </div>
            <Slider {...settings}>
                {productions.map((el, idx) => (
                    <div className="card" style={{ width: 200 }} key={idx}>
                        <span className="absolute top-0 text-white ">{idx}</span>
                        <figure className="pr-5">
                            {/* <img src="https://picsum.photos/id/1005/300/200" className="rounded-lg shadow-lg" /> */}
                            <img src={el.poster} className="rounded-lg shadow-lg" />
                        </figure>
                    </div>
                ))}
            </Slider>


            <div className="card">
                <figure className="p-6">
                    <img src="https://picsum.photos/id/1005/300/200" className="rounded-lg shadow-lg" />
                </figure>
            </div>
        </div >
    );
}

export default ProductionSlider;


