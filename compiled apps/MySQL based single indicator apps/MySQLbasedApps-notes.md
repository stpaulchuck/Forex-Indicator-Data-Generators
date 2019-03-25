# MySQL based indicator apps - notes

I wrote these some long time back when I was using MS Sql Server and then MySQL Server as data holders for several desktop apps I created to analyze Forex currency pair price data looking for patterns to increase my win/loss ratio. SQLite was not considered professional at the time and csv files were frowned upon for serious work. Times change.

I did not convert these apps to use either SQLite or csv files as I wrote another app and it does, as well as consolidating the generators into one app. I am combing through these to ensure they all work with MySQL Server. If I don't refactor any of them I'll leave a note here about it.

## Operational notes

**Server Names**

First off, the server list in the apps needs to be updated to your server host names. It is found in the [applicationname].exe.config file. It has this pattern and is located in the `<applicationSettings>` section.

```html
    <setting name="ServerList" serializeAs="Xml">
        <value>
            <ArrayOfString xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <string>chucksgateway:3303</string>
            </ArrayOfString>
        </value>
    </setting>
```

Use a plain text editor like Notepad or Notepad++ to work with this file. Just add to the list of strings using the pattern you see here with server names or edit the one you find there. If your server is on a port other than 3306, then you need to add the port number to the server name separated by a colon, like this - "myserverhostname:3309". Note there are no quote marks around the string value in the config file.

**Username and Password**

You'll find those two items in the config file as well. You can set them to whatever you like but remember that MySQL is case sensitive on usernames too! (at least as of this date)

**User Settings section**

don't worry about the values found there. They will be changed by the program when you change them in the application.