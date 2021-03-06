﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowVictor.Domain.Entities
{
    public class Account : IEntity
    {   
        public Guid Id{get;private set;}

        public Account()
        {
            Id = Guid.NewGuid();
            Activated = false;
            RegisterDate= DateTime.Now;
            LastSeen = DateTime.Now;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Activated { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Views { get; set; }
        public DateTime LastSeen { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
