# ItemRoulette
Console Application where you can add items to a datafile and spin the roulette to have 2 items compared against each other.

Create your own User with User settings that change how the information is displayed to you. Once you have created Items in the Item Creator you will earn Roulette Credits. Use Roulette Credits in the Item Roulette to spin the roulette and have items shown to you with some comparison made. 

**Feature List:**
- Implement a "Master Loop" (Navigator)
- Create an class that inherits from a parent (All the Menus derive from Menu)
- Create a dictionary or list (Create lists of Items and Users)
- Read data from external file (Reads and Writes UserList/ItemList to JSON in Data folder)
- Use a LINQ query (Used significantly in the ItemRouletteMenu)
- Build a conversion tool (MeasurementConverter & TemperatureConverter) 

**Running the Application:**<br />
The application does not run on any OS other than Windows due to pathing constraints.
When the application is built it copies the ItemData and UserData stored in the solution to the active directory. If for some reason this doesn't happen you can do it manually by copying the folder "Data" in the solution explorer to the bin/debug/.net folder directly. The application is not meant to run without ItemData being populated and you will not be able to use the Item Roulette Menu until at least 10 items have been created.
