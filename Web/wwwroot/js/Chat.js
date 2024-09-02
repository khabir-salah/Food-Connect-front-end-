"use script";
let connection;

document.addEventListener("DOMContentLoaded", function () {
    console.log("DOMContentLoaded event fired.");

    const token = document.getElementById("jwtToken").value;
    const donationId = document.getElementById('donationId').value;
    const userId = document.getElementById('userId').value;

    if (!token || !donationId) {
        console.error("JWT Token or Donation ID is missing.");
        return;
    }

    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7005/chathub", {
            accessTokenFactory: () => token,
            withCredentials: true
        })
        .build();

    connection.start().then(() => {
        console.log("SignalR connection started.");
        return connection.invoke("InitializeConnection", donationId)
            .catch(err => console.error("Failed to initialize connection:", err.toString()));
    }).catch(err => console.error("Failed to start SignalR connection:", err.toString()));

    connection.serverTimeoutInMilliseconds = 2 * 60 * 60 * 1000;

    connection.keepAliveIntervalInMilliseconds = 30 * 60 * 1000; 

    // Listen for incoming messages
    connection.on("ReceiveMessage", function (message) {
        console.log("ReceiveMessage event triggered.");
        console.log("New message received:", message);

        const messageElement = document.createElement("div");
        const alignmentClass = message.userId === userId ? "sender" : "receiver";
        messageElement.className = `message ${alignmentClass}`;

        const messageContent = document.createElement("div");
        messageContent.className = "message-content";
        messageContent.textContent = message.message;

        messageElement.appendChild(messageContent);

        // Add timestamp
        const timestamp = document.createElement("small");
        timestamp.textContent = new Date(message.timestamp).toLocaleString();
        timestamp.style.display = "block";
        messageContent.appendChild(timestamp);

        const chatMessages = document.getElementById("chatMessages");
        if (chatMessages) {
            chatMessages.appendChild(messageElement);
        } else {
            console.error("chatMessages element not found.");
        }
    });

});

document.getElementById('sendMessageButton').addEventListener('click', function () {
    console.log("SendMessageButton clicked.");

    const donationId = document.getElementById('donationId').value;
    const content = document.getElementById('messageContent').value;
    const userId = document.getElementById('userId').value;

    if (!content) {
        alert("Please enter a message.");
        return;
    }

    if (!connection) {
        console.error("SignalR connection is not established.");
        return;
    }

    connection.invoke("SendMessage", {
        donationId: donationId,
        content: content,
        UserId: userId
    }).then(() => {
        console.log("Message sent successfully.");
        document.getElementById('messageContent').value = ''; 
    }).catch(function (err) {
        console.error("Failed to send message:", err.toString());
    });
});



//let connection;

//document.addEventListener("DOMContentLoaded", function () {
//    console.log("DOMContentLoaded event fired.");

//    const token = document.getElementById("jwtToken").value;
//    const donationId = document.getElementById('donationId').value;
//    //const donationId = document.getElementById('recipient').value;

//    if (!token || !donationId) {
//        console.error("JWT Token or Donation ID is missing.");
//        return;
//    }

//    connection = new signalR.HubConnectionBuilder()
//        .withUrl("https://localhost:7005/chathub", {
//            accessTokenFactory: () => token,
//            withCredentials: true
//        })
//        .build();

//    connection.start().then(() => {
//        console.log("SignalR connection started.");
//        return connection.invoke("InitializeConnection", donationId)
//            .catch(err => console.error("Failed to initialize connection:", err.toString()));
//    }).catch(err => console.error("Failed to start SignalR connection:", err.toString()));

//    connection.serverTimeoutInMilliseconds = 9000000;

//    connection.on("ReceiveMessage", function (message) {
//        console.log("ReceiveMessage event triggered.");
//        console.log("New message received:", message);

//        const messageElement = document.createElement("div");
//        messageElement.className = "message text-left";

//        const messageContent = document.createElement("div");
//        messageContent.className = "message-content";

//        //const messageElement = document.createElement("div");
//        //messageElement.className = `message ${message.sender === recipient ? 'text-left' : 'text-right'}`;

//        messageContent.textContent = message.message;

//        messageElement.appendChild(messageContent);

//        // Add timestamp
//        const timestamp = document.createElement("small");
//        timestamp.textContent = new Date().toLocaleString();
//        timestamp.style.display = "block";
//        messageContent.appendChild(timestamp);

//        const chatMessages = document.getElementById("chatMessages");
//        if (chatMessages) {
//            chatMessages.appendChild(messageElement);
//        } else {
//            console.error("chatMessages element not found.");
//        }
//    });

//});

//document.getElementById('sendMessageButton').addEventListener('click', function () {
//    console.log("SendMessageButton clicked.");

//    const donationId = document.getElementById('donationId').value;
//    const content = document.getElementById('messageContent').value;

//    if (!content) {
//        alert("Please enter a message.");
//        return;
//    }

//    if (!connection) {
//        console.error("SignalR connection is not established.");
//        return;
//    }

//    connection.invoke("SendMessage", {
//        donationId: donationId,
//        content: content
//    }).then(() => {
//        console.log("Message sent successfully.");
//        document.getElementById('messageContent').value = ''; // Clear input after sending
//    }).catch(function (err) {
//        console.error("Failed to send message:", err.toString());
//    });
//});


//connection.on("ReceiveMessage", function (message) {
//    console.log("ReceiveMessage event triggered.");
//    console.log("New message received:", message);

//    const messageElement = document.createElement("div");

//    const alignmentClass = message.donorId ===  ? "text-right sender" : "text-left receiver";
//    messageElement.className = `message ${alignmentClass}`;

//    const messageContent = document.createElement("div");
//    messageContent.className = "message-content";
//    messageContent.textContent = message.message;

//    messageElement.appendChild(messageContent);

//    // Add timestamp
//    const timestamp = document.createElement("small");
//    timestamp.textContent = message.sentAt;
//    messageContent.appendChild(timestamp);

//    const chatMessages = document.getElementById("chatMessages");
//    if (chatMessages) {
//        chatMessages.appendChild(messageElement);
//    } else {
//        console.error("chatMessages element not found.");
//    }
//});

 // Get the user ID from Razor page

//connection.on("ReceiveMessage", function (message) {
//    console.log("ReceiveMessage event triggered.");
//    console.log("New message received:", message);

//    const messageElement = document.createElement("div");

//    const alignmentClass = message.donorId === userId ? "text-right sender" : "text-left receiver";
//    messageElement.className = `message ${alignmentClass}`;

//    const messageContent = document.createElement("div");
//    messageContent.className = "message-content";
//    messageContent.textContent = message.Message; // Correct the property to access the message content

//    messageElement.appendChild(messageContent);

//    // Add timestamp
//    const timestamp = document.createElement("small");
//    timestamp.textContent = message.Timestamp;
//    messageContent.appendChild(timestamp);

//    const chatMessages = document.getElementById("chatMessages");
//    if (chatMessages) {
//        chatMessages.appendChild(messageElement);
//    } else {
//        console.error("chatMessages element not found.");
//    }
//});




//document.getElementById('sendMessageButton').addEventListener('click', function () {
//    const donationId = document.getElementById('donationId').value;
//    const content = document.getElementById('messageContent').value;

//    if (content) {
//        // Send message via SignalR
//        connection.invoke("SendMessage", {
//            donationId: donationId,
//            content: content
//        }).catch(function (err) {
//            return console.error(err.toString());
//        });

//        // Clear the message input after sending
//        document.getElementById('messageContent').value = '';

//    } else {
//        alert("Please enter a message.");
//    }
//});
