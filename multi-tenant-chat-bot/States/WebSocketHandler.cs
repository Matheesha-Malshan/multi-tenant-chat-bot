using System.Net.WebSockets;

namespace multi_tenant_chatBot.States;

public class WebSocketHandler
{
    
    public static async Task Handle(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = 400;
            return;
        }

        using WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();

        WebSocketManager.AddSocket(socket);

        var buffer = new byte[1024 * 4];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                    "Closing", CancellationToken.None);
            }
        } 
        
    }
}