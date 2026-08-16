// Options are the answers to a question.
// Each option has a value. Positive moves your score in one direction, negative moves it in the other. The value is used to calculate the final score of the quiz.
// These are doubles. +/- 1.0 is for extreme answers, +/- 0.5 is for moderate answers, and 0.0 is used only for five-option neutral answers. For four-option questions, the scale is -1.0, -0.5, 0.5, and 1.0. The final score is the sum of all option values selected by the user.
public class Option
{
    private readonly string text;
    private readonly double value;

    public string Text => text;
    public double Value => value;

    public Option(string text, double value)
    {
        this.text = text;
        this.value = value;
    }

    public override string ToString()
    {
        return text;
    }
}

public class Question
{
    private readonly string text;
    // isEcon is true if the question is about economics, false if it is about auth/lib.
    private readonly bool isEcon;
    private readonly List<Option> options = new List<Option>();

    public string Text => text;
    public bool IsEcon => isEcon;
    public IReadOnlyList<Option> Options => options;

    public Question(string text, bool isEcon, List<Option> options)
    {
        this.text = text;
        this.isEcon = isEcon;
        this.options = options;
    }

    public override string ToString()
    {
        return text;
    }
}

public class QuestionCompiler
{
    public QuestionCompiler()
    {
    }

    public List<Question> Build()
    {
        List<Question> questions = new List<Question>();

        questions.Add(new Question(
            "High-income earners should be taxed at a higher percentage.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Essential industries such as electricity, water, and public transportation should be publicly owned.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "The government should cover healthcare costs...",
            true,
            new List<Option>
            {
                new Option("Completely", -1.0),
                new Option("Mostly", -0.5),
                new Option("Partially", 0.5),
                new Option("Never", 1.0)
            }
        ));

        questions.Add(new Question(
            "Minimum wage should be increased.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Private ownership of real estate should be legal.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should provide financial assistance to people who cannot support themselves financially.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Labor unions are beneficial to workers...",
            true,
            new List<Option>
            {
                new Option("Always", -1.0),
                new Option("In most fields", -0.5),
                new Option("In some fields", 0.5),
                new Option("Never", 1.0)
            }
        ));

        questions.Add(new Question(
            "Public universities should not charge tuition.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "The government should break up businesses when they become dominant in their field.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Individuals should be allowed to own businesses and employ workers for profit.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "There should be a limit on the amount of wealth an individual can obtain.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Businesses should generally be free to set their own prices without government interference.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should prioritize free trade over protecting domestic industries from foreign competition.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should guarantee employment to people who are willing and able to work.",
            true,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "The government should ban speech that directly incites violence.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should ban speech that is offensive or hateful.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The following recreational drugs should be legal to purchase...",
            false,
            new List<Option>
            {
                new Option("All of them", -1.0),
                new Option("Most of them", -0.5),
                new Option("Some", 0.5),
                new Option("No recreational drugs", 1.0)
            }
        ));

        questions.Add(new Question(
            "The government should have the authority to dissolve political parties that it considers a threat to national security.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "Individuals should have the right to own firearms...",
            false,
            new List<Option>
            {
                new Option("Always", -1.0),
                new Option("Most of the time", -0.5),
                new Option("Rarely", 0.5),
                new Option("Never", 1.0)
            }
        ));

        questions.Add(new Question(
            "The government should be able to forcibly disperse a riot...",
            false,
            new List<Option>
            {
                new Option("Always", 1.0),
                new Option("Most of the time", 0.5),
                new Option("Rarely", -0.5),
                new Option("Never", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should determine what subjects and ideas public-school teachers are permitted to teach.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "Individuals should be able to publicly practice and preach their religion.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Capital punishment should be legal.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "Prisoners should have a right to vote.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Citizens should be able to directly elect most government officials.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", -1.0),
                new Option("Agree", -0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", 0.5),
                new Option("Strongly Disagree", 1.0)
            }
        ));

        questions.Add(new Question(
            "Public areas should be under surveillance to deter and investigate crime.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should be able to punish people for deliberately spreading false information.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        questions.Add(new Question(
            "The government should be able to conscript citizens into the military when necessary.",
            false,
            new List<Option>
            {
                new Option("Strongly Agree", 1.0),
                new Option("Agree", 0.5),
                new Option("Indifferent", 0.0),
                new Option("Disagree", -0.5),
                new Option("Strongly Disagree", -1.0)
            }
        ));

        var rng = new Random();
        return questions
            .OrderBy(_ => rng.Next())
            .ToList();
    }
}