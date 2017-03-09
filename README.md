# _**Ticketizer**_

### By Chad T. Durkin, Lawrence Eby, Melvin F. Gruschow, Grinil Khanna

# Description
Ticketizer is a customer service applications for a company to organize consumer interactions. It is intended to help minimize negative consumer experiences by allowing a company to quickly and efficiently go through consumer "tickets" and responding appropriately.

# Specs

* An admin can create a ticket with information about a consumer and have that consumer's information saved.
* An admin can create knowledge articles to help keep track of common problems.
* An admin can attach notes and knowledge articles to specific tickets to keep track of a ticket's history.
* An admin can close tickets and find them in the archive.
* An admin can search for specific tickets by ticket number.
* An admin can search for knowledge articles.
* An admin can search a specific consumer name and auto-populate data.
* An admin can log in and have their "state" saved, allowing them to interact with the rest of the program.
* An admin can send an email to a consumer directly from the app.
* An admin can create departments and search articles based on department.
* An admin can browse articles inside and outside of a ticket.
* An admin can update a ticket's information.
* An admin can see states for the week.

# Installation
- Clone repository
- In SQLCMD:
    * CREATE DATABASE ticketizer;
    * GO


- Use the scripts file "db_scripts.sql" in your SQL server on the database title "ticketizer"
- On a dnvm ready machine, use "dnx kestrel" in the command line.
- Run Localhost:5004
- (Optional for demo purposes) Click the red circle in the bottom right-hand corner to autopopulate demo data.

# Technologies Used
* HTML
* CSS
* MATERIALIZE
* C#
* SQL
* NANCY
* RAZOR View Engine

# License
Open Source under the condition that you tell us that you're using it, otherwise, it's $10k.

Copyright (c) 2017 Chad T. Durkin, Lawrence Eby, Melvin F. Gruschow, and Grinil Khanna All Rights Reserved.
