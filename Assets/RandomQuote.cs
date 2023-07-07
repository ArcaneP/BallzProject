using UnityEngine;
using TMPro;

public class RandomQuote : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string[] quotes = {
    /*1*/ "“Character consists of what you do on the third and fourth tries.” ― James A. Michener",
    /*2*/ "“It always seems impossible until it’s done.” ― Nelson Mandela",
    /*3*/ "“How long should you try? Until.” – Jim Rohn",
    /*4*/ "“People are always blaming circumstances for what they are. I don’t believe in circumstances. The people who get ahead in this world are the people who get up and look for the circumstances they want, and if they can’t find them, make them.” – George Bernard Shaw",
    /*5*/ "“Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful.” – Albert Schweitzer",
    /*6*/ "“The harder you work for something, the greater you'll feel when you achieve it.” – Unknown",
    /*7*/ "“The only person you are destined to become is the person you decide to be.” – Ralph Waldo Emerson",
    /*8*/ "“Dream big and dare to fail.” – Norman Vaughan",
    /*9*/ "“Believe you can and you're halfway there.” – Theodore Roosevelt",
    /*10*/ "“The only limit to our realization of tomorrow will be our doubts of today.” – Franklin D. Roosevelt",
    /*11*/ "“The future belongs to those who believe in the beauty of their dreams.” – Eleanor Roosevelt",
    /*12*/ "“The only way to do great work is to love what you do.” – Steve Jobs",
    /*13*/ "“The journey of a thousand miles begins with one step.” – Lao Tzu",
    /*14*/ "“Success is not final, failure is not fatal: It is the courage to continue that counts.” – Winston Churchill",
    /*15*/ "“The biggest risk is not taking any risk. In a world that is changing quickly, the only strategy that is guaranteed to fail is not taking risks.” – Mark Zuckerberg",
    /*16*/ "“The only place where success comes before work is in the dictionary.” – Vidal Sassoon",
    /*17*/ "“Don't watch the clock; do what it does. Keep going.” – Sam Levenson",
    /*18*/ "“The best revenge is massive success.” – Frank Sinatra",
    /*19*/ "“Your time is limited, don't waste it living someone else's life.” – Steve Jobs",
    /*20*/ "“Believe in yourself and all that you are. Know that there is something inside you that is greater than any obstacle.” – Christian D. Larson",
    /*21*/ "“You are never too old to set another goal or to dream a new dream.” – C.S. Lewis",
    /*22*/ "“Opportunities don't happen. You create them.” – Chris Grosser",
    /*23*/ "“Sometimes adversity is what you need to face in order to become successful.” – Zig Ziglar",
    /*24*/ "“To be a champ, you have to believe in yourself when nobody else will.” – Sugar Ray Robinson",
    /*25*/ "“It is not wanting to win that makes you a winner; it is refusing to fail.” – Anonymous",
    /*26*/ "“If one dream should fall and break into a thousand pieces, never be afraid to pick one of those pieces up and begin again.” – Flavia Weedn",
    /*27*/ "“The only thing you should ever quit is giving up!” – Steve Pfiester",
    /*28*/ "“It’s not that I’m so smart, it’s just that I stay with problems longer.” – Albert Einstein",
    /*29*/ "“When you are going through hell, keep on going. Never never never give up.” – Winston Churchill",
    /*30*/ "“Don’t be discouraged. It’s often the last key in the bunch that opens the lock.” – Anonymous",
    /*31*/ "“A winner is someone who gets up one more time than he is knocked down.” – Anonymous",
    /*32*/ "“Our greatest glory is not in never falling but in rising every time we fall.” – Confucius",
    /*33*/ "“Never give up, for that is just the place and time that the tide will turn.” – Harriet Stowe",
    /*34*/ "“Difficult things take a long time, impossible things a little longer.” – Anonymous",
    /*35*/ "“Many of life’s failures are people who did not realize how close they were to success when they gave up.” – Thomas Edison",
    /*36*/ "“I am not discouraged because every wrong attempt discarded is a step forward.” – Thomas Edison",
    /*37*/ "“I have not failed, I have just found 10,000 ways that won’t work.” – Thomas Edison",
    /*38*/ "“It is never too late to be what you might have been.” – George Eliot",
    /*39*/ "“Nothing could be worse than the fear that one had given up too soon, and left one unexpended effort that might have saved the world.” – Jane Addams",
    /*40*/ "“The day you give up on your dreams is the day you give up on yourself.” – Anonymous",
    /*41*/ "“Life is 10% what happens to us and 90% how we react to it.” – Charles R. Swindoll",
    /*42*/ "“Believe in yourself, take on your challenges, dig deep within yourself to conquer fears. Never let anyone bring you down. You got this.” – Chantal Sutherland",
    /*43*/ "“The secret of success is to know something nobody else knows.” – Aristotle Onassis",
    /*44*/ "“You can’t go back and change the beginning, but you can start where you are and change the ending.” – C.S. Lewis",
    /*45*/ "“The only way to achieve the impossible is to believe it is possible.” – Charles Kingsleigh",
    /*46*/ "“Every great dream begins with a dreamer. Always remember, you have within you the strength, the patience, and the passion to reach for the stars to change the world.” – Harriet Tubman",
    /*47*/ "“Your work is going to fill a large part of your life, and the only way to be truly satisfied is to do what you believe is great work. And the only way to do great work is to love what you do.” – Steve Jobs",
    /*48*/ "“You miss 100% of the shots you don’t take.” – Wayne Gretzky",
    /*51*/ "“In the middle of difficulty lies opportunity.” – Albert Einstein",
    /*52*/ "“I find that the harder I work, the more luck I seem to have.” – Thomas Jefferson",
    /*53*/ "“The best way to predict your future is to create it.” – Peter Drucker",
    /*54*/ "“The successful warrior is the average man, with laser-like focus.” – Bruce Lee",
    /*55*/ "“It’s not about how hard you hit. It’s about how hard you can get hit and keep moving forward.” – Rocky Balboa",
    /*57*/ "“Don’t watch the clock; do what it does. Keep going.” – Sam Levenson",
    /*58*/ "“The only person you should try to be better than is the person you were yesterday.” – Unknown",

    /*59*/ "“You cant just make up a quote and use it in your own game said noone ever”– Skiller",
    /*60*/ "”There was 0.244% chance that you will get this quote so consider yourself super lucky.”- Developer"
};

    private void Start()
    {
        if (text == null)
        {
            Debug.LogError("TextMesh component not assigned!");
            return;
        }

        if (quotes == null || quotes.Length == 0)
        {
            Debug.LogError("Quotes array is empty!");
            return;
        }

        SetRandomQuote();
    }

    private void SetRandomQuote()
    {
        int randomIndex = Random.Range(0, quotes.Length);
        string randomQuote = quotes[randomIndex];
        text.text = randomQuote;
    }
}
