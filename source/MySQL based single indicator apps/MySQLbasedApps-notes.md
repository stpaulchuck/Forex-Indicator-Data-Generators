# MySQL based indicator apps - notes

I wrote these some long time back when I was using MS Sql Server and then MySQL Server as data holders for several desktop apps I created to analyze Forex currency pair price data looking for patterns to increase my win/loss ratio. SQLite was not considered professional at the time and csv files were frowned upon for serious work. Times change.

I did not convert these apps to use either SQLite or csv files as I wrote another app and it does, as well as consolidating the generators into one app. I am combing through these to ensure they all work with MySQL Server. If I don't refactor any of them I'll leave a note here about it.

## Operational notes

**Server Names**

First off, the server list in the apps needs to be updated to your server host names. It is found in the application properties file on the settings tab. It is a string list object. Edit the one you find there and add any secondary or tertiary servers you would like in the list. If your server is on a port other than 3306, then you need to add the port number to the server name separated by a colon, like this - "myserverhostname:3309". Note there are no quote marks around the string values in the settings file.

**Username and Password**

You'll find those two items in the properties-settings as well. You can set them to whatever you like but remember that MySQL is case sensitive on usernames too! (at least as of this date)

**User Settings**

don't worry about the values found there. They will be changed by the program when you change them in the application.