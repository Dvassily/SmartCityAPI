using Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class InterestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static InterestDTO FromInterest(Interest interest)
            => new InterestDTO
        {
            Id = interest.Id,
            Name = interest.Name
        };
    }
}
