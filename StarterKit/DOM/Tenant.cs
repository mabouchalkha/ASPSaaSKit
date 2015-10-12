﻿using StarterKit.Architecture.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterKit.DOM
{
    public class Tenant : IIdentifiableEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public bool IsTrial { get; set; }

        public Guid EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
