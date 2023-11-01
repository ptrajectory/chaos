# CHAOS
- Chat functionality is almost always never part of your core business needs, its just a piece of functionality you need to complete a specific subset of your business requirements. So all the code you write for chat functionality as well as all the sprints you take working on it may be wasted time 🤷‍♂️ (I know hot take, but hear me out).
- Chat functionality is universal and regardless of your implementation the results will always be the same. You need to provide some sort of text based communication between 2 or more individuals. This maybe realtime or otherwise, but whatever you implementation will be the end result will always be the same. 
- Now, this truth is the foundation of many libraries and SaaS products, just like CHAOS
- Initially this project was called CHAAS i.e to be an acronymn for chat as a service; but that's a 💩 name, so I changed...(you know what I think you can figure out what I changed 🙃 )
- The AIM of this project was to build a universal Chat Service API that I could use as a dependancy for my other side projects that required Chat functionality.
- And I think going forward a lot of applications in the wild will have some sort of Chat functionality inorder to incorparate ai agents into their fabric.
- So its important to provide a fast, reliable, fast and feature rich launching pad, for all the cool chat driven applications out their.
- Before we dive into all the cool endpoints that this service provides its important to break it down into its pieces, and build it back up one at a time.

## Entities
- In case you haven't picked it up by now, this is gonna be opinionated 😁
- I've tried to break the entire chat system into the following entities:
  - Organizations
  - Apps
  - Channels
  - Users
  - Participants

### Organizations
- Owners of applications need a way to organize all the applications they own. Users can then create applications under the organizations they own.
- Here is a breakdown of the organization entity has the following fields:
  - id
    - a unique identifier of the organization, usually a 16char hex string prefixed with ``org_``
  - name
    - a user provided name of the organization
  - email
    - the user's email
  - banner
    - A banner for the organization
  - created_at
    - When the organization was created

### Applications
- Applications in this context are meant to organize functionality for a single... well application. e.g if working on your next cool chat bot the first thing u'll need to do is create an application for that use case.
