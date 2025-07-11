﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskDisc.Core.Entities;
public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int ID_JobTitle { get; set; }

    public JobTitlesEntity JobTitle { get; set; }
    public ICollection<TaskUserEntity> TaskUsers { get; set; }
    public ICollection<TokenEntity> Tokens { get; set; }
}