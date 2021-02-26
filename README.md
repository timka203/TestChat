# TestChat

StepChat- приложение для общения состоящее из 4 частей. 

StepChat.Common - набор классов для работы StepChat.Server и Stepchat.UI2_2. Обязателен для обоих приложений

StepChat.Server - приложение запускаемое на стороне сервера, которое перенаправляет сообщения от пользователя к пользователю.

StepChat.UI2_2- приложения запускаемое на устройстве пользователя, которое отпраляет и получает сообщения.

StepChat.Web_2_0 - ASP.NET приложение для регистрации пользователей.

Для работы приложения надо изменить путь к файлу MyDB.mdf в App.config в StepChat.Server  и в Web.config в StepChat.Web_2_0.
Также стоит поменять url к которому будет подключаться WebSocket  в AccountLogInResource, в CreateGroup, и в ChatWebServer которые 
находятся в StepChat.Server(везде должен быть одинаковый url, который должен начинаться с ws:// ),
и uri  в Programm в папку ManualHTTPServer
Потом в StepChat.UI2_2 в файле WebSocketForUser нужно поменять url для websocket которй вы указывали раньше.

StepChat.UI2_2 и StepChat.Server работают отдельно друг от друга, и могут быть запущены на разных устройствах, при условии
правильной настройки url и  uri


StepChat is a 4-part communication application.

StepChat.Common - a set of classes for StepChat.Server and Stepchat.UI2_2. Required for both apps

StepChat.Server is a server-side application that redirects messages from user to user.

StepChat.UI2_2- an application launched on the user's device that sends and receives messages.

StepChat.Web_2_0 - ASP.NET application for user registration.

For the application  work, you need to change the path to the MyDB.mdf file in the App.config in StepChat.Server and in the Web.config in StepChat.Web_2_0.
It is also worth changing the url to which the WebSocket will connect in AccountLogInResource, in CreateGroup, and in ChatWebServer which
located in StepChat.Server (everywhere there must be the same url, which must start with ws: //),
and uri in Programm in ManualHTTPServer folder
Then in StepChat.UI2_2 in the WebSocketForUser file you need to change the url for the websocket you specified earlier.

StepChat.UI2_2 and StepChat.Server work separately from each other, and can be launched on different devices, provided
correct url and uri setting
