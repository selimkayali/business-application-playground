## Business Requirements
- As a Book store user I should be able to generate a Word (.docx) report that is displays a list of books filtered by my search criteria.
- As a Book store user I should be able to search books by:
* publication year:
  * Before a given year '<='
  * after a given year '>='
* author (AuthorId)
* whether it is best seller or not.
- As a Book store user I should at least provide one search criteria to generate report.
- As a Book store user I should be able to see the Word report that is generated with user friendly formatting.

Example for formatting:

Title | Author | Price | Best Seller | Availability
--- | --- | --- | --- | ---
 Clean Code: A Handbook of Agile Software Craftsmanship | Robert C. Martin | €28.63 | Not Bestseller | Not available in stock  
 A Game of Thrones: A Song of Ice and Fire, Book 1 | George R. R. Martin | €29.99 | Bestseller | Available in stock (5) 

## Technical Constraints
- Design a web api endpoint that receives filter information and returns the generated report.
- Organize your code using folders and namespaces as you see fit, no new project should be introduced to the solution.
- Product owner informed you that in the future the report might be PDF format.
- Use the migration script provided for database structure.
- Implement unit tests as required.
- use git for source control.
- Choose a NuGet Package for Word generation, explain briefly the reason of your choice.
- See example file for reference ExampleFile/BookSearchReport-2019-10-10-06-50.docx
- Feel free to ask any clarifications as needed.
