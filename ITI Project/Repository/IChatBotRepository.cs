namespace ITI_Project.Repository
{
    public interface IChatBotRepository
    {
        Task<string> AskGeminiAsync(string message);
    }
}
