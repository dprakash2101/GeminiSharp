# 💬 Chat Sessions with GeminiSharp

GeminiSharp provides a `ChatSession` class to manage multi-turn conversations with the Gemini API. This class simplifies the process of maintaining conversation history and sending messages, making it ideal for building interactive chat applications.

---

## 🔑 Key Components

*   **`ChatSession`**: This class encapsulates the conversation history and provides a `SendMessageAsync` method to interact with the Gemini model.
*   **Conversation History**: The `ChatSession` automatically appends user messages and model responses to its internal history, ensuring that subsequent turns in the conversation have the necessary context.

---

## 🧑‍💻 C# Example: Starting and Continuing a Chat Session

This example demonstrates how to start a new chat session, send messages, and retrieve responses while maintaining the conversation history.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ChatSessionExample
{
    public static async Task Run(string apiKey)
    {
        // Initialize GeminiClient
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        string model = "gemini-2.5-flash"; // Or any other suitable chat model

        Console.WriteLine($"\n--- Chat Session Example (Model: {model}) ---\n");

        try
        {
            // Start a new chat session
            Console.WriteLine("Starting a new chat session...");
            ChatSession chat = geminiClient.StartChat(model);
            Console.WriteLine("Chat session started. Type 'exit' to end the conversation.\n");

            while (true)
            {
                Console.Write("You: ");
                string? userMessage = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userMessage) || userMessage.ToLower() == "exit")
                {
                    Console.WriteLine("Ending chat session.");
                    break;
                }

                // Send message and get response
                var response = await chat.SendMessageAsync(userMessage);

                // Display model's response
                string? modelResponse = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
                if (!string.IsNullOrWhiteSpace(modelResponse))
                {
                    Console.WriteLine($"Gemini: {modelResponse}");
                }
                else
                {
                    Console.WriteLine("Gemini: (No response or an error occurred)");
                }
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error during chat: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred during chat: {ex.Message}");
        }
    }
}

// To run this example:
// ChatSessionExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of `ChatSession`**

✅ **Automatic History Management**: The `ChatSession` class automatically manages the conversation history, sending previous turns with each new message to provide context to the model.
✅ **Simplified API Calls**: Abstracts away the complexities of manually constructing `GenerateContentRequest` objects for each turn.
✅ **Multi-Turn Conversations**: Enables seamless multi-turn interactions, allowing the model to remember and build upon previous exchanges.

---

## **📌 Important Considerations**

*   **Token Limits**: Be mindful of the model's token limits. As the conversation history grows, it consumes more tokens. Long conversations might eventually hit the model's maximum input token limit.
*   **Model Choice**: Ensure you select a Gemini model that is suitable for chat applications (e.g., `gemini-2.5-flash` or other models optimized for conversational AI).
*   **Error Handling**: Implement robust error handling to gracefully manage API errors or unexpected responses during the chat.

---

