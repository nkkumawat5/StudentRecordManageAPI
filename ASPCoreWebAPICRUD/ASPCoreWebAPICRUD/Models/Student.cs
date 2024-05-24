﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreWebAPICRUD.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }

    public string? StudentName { get; set; }

    public string? StudentGender { get; set; }

    public int? Age { get; set; }

    public int? Standard { get; set; }

    public string? FatherName { get; set; }
}
