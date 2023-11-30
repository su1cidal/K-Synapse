public class QuizManager
{
    public static QuizManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    
}