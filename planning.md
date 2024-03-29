# Planning

## Clarifications
*Quoted answers come from [ChatGPT](https://chat.openai.com/) who plays the role of external stakeholder (i.e. customer).*

- Is the application for servers or for the people making the drinks?
  - >The application is for the restaurant's employees who make drinks. They use it to access information about various drinks available in the restaurant's database, making their workflow more efficient.
- Will there be multiple simultaneous users?
  - >Yes, there may be multiple employees using the application simultaneously, especially during peak hours. It should efficiently handle concurrent users accessing drink information from the database.
- Are there any requirements on response time?
  - >The application should provide quick responses to ensure efficient service for employees. It's important that the response time is fast enough to support a smooth workflow during busy periods in the restaurant.
- Should there be any caching?
  - >If caching can improve the application's speed and responsiveness, it's a good idea. However, ensure that the information remains up-to-date to provide accurate details about the drinks in the restaurant's database.
- The main use case centers around the user finding a drink by category, yes?
  - >Exactly. The primary use case is for employees to easily locate drinks by selecting a category. This streamlines the process and helps them access relevant information quickly, enhancing their efficiency.
- Should there be functionality for searching drinks by name?
  - >Yes, it would be beneficial to include a search functionality by name. This allows employees to find specific drinks swiftly, adding flexibility to the application and catering to varied preferences.
- Should there be an alphabetical listing of drinks?
  - >Yes, having an alphabetical listing of drinks would be helpful for easy navigation. It provides an additional way for employees to browse through the available drinks, enhancing the user-friendly nature of the application.
- Should users be able to search by ingredient?
  - >Certainly. Including a search by ingredient feature would be beneficial for employees who want to explore drinks based on specific ingredients. It adds versatility to the application, accommodating different preferences.
- Should users be able to filter by category?
  - >Yes, implementing a filter by category option is important. It helps employees narrow down their drink choices, making it more convenient to find what they need based on specific categories, enhancing the overall usability.
- Should users be able to filter by alcoholic/non-alcoholic?
  - >Certainly. Including a filter for alcoholic and non-alcoholic drinks is valuable. It allows employees to quickly distinguish between different types of drinks, aligning with the preferences and requirements of our customers.
- Should users be able to filter by type of glass?
  - >Yes, adding a filter by the type of glass is a good idea. It enhances the user experience, helping employees choose drinks suitable for specific glassware and ensuring a cohesive presentation in the restaurant.
- Should users be able to filter by ingredient?
  - >Yes, having the ability to filter drinks by ingredient is important. It provides employees with a convenient way to explore and discover drinks based on specific ingredients, catering to diverse preferences.
- The database has some information in other languages like German or Italian. Should this information be shown?
  - >No, it's preferable to display information in a language understood by our employees, English. This ensures clarity and ease of use, aligning with the language proficiency of the restaurant staff.
- Some instructions are measured in fluid ounces, some in centiliters. Should they be converted to all be one or the other?
  - >Yes, it would be more convenient for our employees if all measurements are standardized to either fluid ounces or centiliters. Consistency in units makes the information more accessible and avoids potential confusion.

### Follow-up Questions
- What if the DB/API is unavailable?
  - Nothing to do, so show an error screen.
- What's a good cache lifetime?
  - Drinks shouldn't change, so 24 hours seems fine.
- How about having the application be navigable using only arrow keys (for those who so wish)? I.e. item selection and information scrolling using up-down, and screen switching using left-right (back-forward).
  - >That sounds user-friendly. Allowing navigation with arrow keys provides a straightforward and intuitive way for employees to move through the application. It enhances accessibility and accommodates various preferences in how users interact with the system.
- What if a drink is not found when searching?
  - Simply show a "no results" message.

## MVP Functionality
- Console menu system & data presentation & user input
- HTTP API reading
- Caching
- Searching
- Filtering
- Unit conversion
- Error handling & missing data handling

## User Interface
Drinks category menu:
```text
  +-----CATEGORIES------+
  | Beer                |
  +---------------------+
>>| Cocktail            |<<
  +---------------------+
  | Cocoa               |
  +---------------------+
  | Coffee / Tea        |
  +---------------------+
  | Homemade Liqueur    |
  +---------------------+
  | Ordinary Drink      |
  +---------------------+
  | Punch / Party Drink |
  +---------------------+
  | Shake               |
  +---------------------+
  | Shot                |
  +---------------------+
  | Soft Drink          |
  +---------------------+
  | Other / Unknown     |
  +---------------------+

--------------------
Press [S] to search,
or [F] to filter.
```

Drink listing:
```text
Cocktail (page 1/20)
--------------------

  +-------------------------------------+
  | 155 Belmont                         |
  +-------------------------------------+
  | 57 Chevy with a White License Plate |
  +-------------------------------------+
  | 747 Drink                           |
  +-------------------------------------+
  | 9 1/2 Weeks                         |
  +-------------------------------------+
>>| A Gilligan's Island                 |<<
  +-------------------------------------+
```

Drink information:
```text
Name: A Gilligan's Island
Category: Cocktail
Alcoholic: yes

Glass: Collins glass

Ingredients:
- Vodka, 1 oz
- Peach schnapps, 1 oz
- Orange juice, 3 oz
- Cranberry juice, 3 oz

Instructions:
Shaken, not stirred!
```

## Logic Checklist
- [x] Know C# programming & basic syntax.
- [x] Know how to make a console menu & take user input.
- [ ] Know how to read data from an HTTP API.

## Data Structures
- Category (string)
- Ingredient
  - Id (int)
  - Name (string)
  - Description (string)
  - Type (string)
  - Alcohol (boolean)
  - AlcoholByVolume (float)
- Glass (string)
- AlcoholType (string)
- Tag (string)
- Drink
  - Id (int)
  - Name (string)
  - AlternateName (string?)
  - Tags (`List<string>`)
  - Category (string?)
  - IBACategory (string?)
  - AlcoholType (string?)
  - Glass (string?)
  - Ingredients (`List<string>`)
  - Measures (`List<string>`)
  - Instructions (`List<string>`)
