# MySQL based indicator apps - notes

I wrote these some long time back when I was using MS Sql Server and then MySQL Server as data holders for several desktop apps I created to analyze Forex currency pair price data looking for patterns to increase my win/loss ratio. SQLite was not considered professional at the time and csv files were frowned upon for serious work. Times change.

I did not convert these apps to use either SQLite or csv files as I wrote another app and it does, as well as consolidating the generators into one app. I am combing through these to ensure they all work with MySQL Server. If I don't refactor any of them I'll leave a note here about it.

## Operational notes

First off, the server list in the apps. It is found in the [applicationname].exe.config file. It has this pattern and is located in the <span style="color:blue">`<applicationSettings>`</span> section.

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