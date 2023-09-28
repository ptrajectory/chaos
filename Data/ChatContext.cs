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

        public DbSet<MediaUploads> MEDIA_UPLOADS {get; set;}

        public DbSet<MessageMedia> MESSAGE_MEDIA {get; set;}

        public DbSet<Organization> ORGANIZATION {get;set;}

        public DbSet<Apps> APPS {get;set;}


        
    }
}