﻿using System;
using System.Collections.Generic;

namespace TaskManagerAPI.Models;

public partial class TaskModel
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
