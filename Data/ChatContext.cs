using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace chaos.Models
{
    public class ChatContext: DbContext
    {

        public ChatContext(DbContextOptions<ChatContext> options): base(options){

        }

        public DbSet<User> USER {get; set;}

        public DbSet<Message> MESSAGE {get; set;}

        public DbSet<Channel> CHANNEL {get; set;}


        public DbSet<Participant> PARTICIPANT {get; set;} 


        
    }
}