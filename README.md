# Tour Planner

Tour Planner is a project developed by a team of two students, which is a graphical-user-interface application based on WPF. The goal of this project is to allow users to create and manage tours (bike, hike, running, or vacation) in advance, as well as keep logs and statistical data of completed tours. 

## Requirements

### Goals
- Implement a graphical-user-interface based on WPF
- Apply the MVVM-pattern in C#
- Implement a layer-based architecture with a UI Layer, a business layer (BL), and a data access layer (DAL)
- Implement design-patterns in your project
- Define your own reusable UI-component
- Store the tour-data and tour-logs in a PostgreSQL database; images should be stored externally on the filesystem
- Use a logging framework like log4net or log4j
- Generate a report by using an appropriate library of your choice
- Generate your own unit-tests with JUnit or NUnit
- Keep your configuration (DB connection, base directory) in a separate config-file - not in the compiled source code
- Document your application architecture and structure as well as the development process and key decisions using UML and wireframes

### Features
- The user can create new tours (no user management, login, registration... everybody sees all tours)
- Every tour consists of name, tour description, from, to, transport type, tour distance, estimated time, route information (an image with the tour map)
  - The image, the distance, and the time should be retrieved by a REST request using the MapQuest Directions and Static Map APIs
- Tours are managed in a list, and can be created, modified, deleted (CRUD)
- For every tour, the user can create new tour logs of the accomplished tour statistics
  - Multiple tour logs are assigned to one tour
  - A tour-log consists of date/time, comment, difficulty, total time, and rating taken on the tour
- Tour logs are managed in a list, and can be created, modified, deleted (CRUD)
- Validated user-input
- Full-text search in tour- and tour-log data
- Automatically computed tour attributes
  - Popularity (derived from number of logs)
  - Child-friendliness (derived from recorded difficulty values, total times and distance)
  - Full-text search also considers the computed values
- Import and export of tour data (file format of your choice)
- The user can generate two types of reports
  - A tour-report which contains all information of a single tour and all its associated tour logs
  - A summarize-report for statistical analysis, which for each tour provides the average time, distance, and rating over all associated tour-logs

## Usage

To use this application, you will need to clone this repository and open it in your preferred IDE. After that, you will need to install the necessary dependencies and set up a PostgreSQL database to store tour data and logs. Once you have set up the environment, you can run the application and start creating tours and tour logs.

## Credits

This project was developed by [Erdem Sevilmis](https://github.com/Erdem-Sevilmis) and [Koale837](https://github.com/Koale8730) as part of SS2023-SWEN2 at [FH Technikum Wien](https://www.technikum-wien.at/). Special thanks to [RaoulHolzer](https://github.com/RaoulHolzer) for the guidance and support throughout the project. 

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request. We welcome all contributions!
