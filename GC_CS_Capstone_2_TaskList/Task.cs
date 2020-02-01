using System;
using System.Collections.Generic;
using System.Text;

namespace GC_CS_Capstone_2_TaskList
{
    public class Task
    {
        #region fields
        // fields

        private string memberName;
        private string taskDescription;
        private DateTime dueDate;
        private bool taskCompleted;
        #endregion

        #region properties
        // properties

        public string MemberName
        {
            get { return memberName; }
            set { memberName = value; }
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public bool TaskCompleted
        {
            get { return taskCompleted; }
            set { taskCompleted = value; }
        }
        #endregion


        #region constructors
        // constructors
        
        // default constructor
        public Task() { }

        // fully loaded constructor
        public Task(string _memberName, string _taskDescription, DateTime _dueDate, bool _taskCompleted)
        {
            memberName = _memberName;
            taskDescription = _taskDescription;
            dueDate = _dueDate;
            taskCompleted = _taskCompleted;
        }

        // Unassigned task
        public Task(string _taskDescription, DateTime _dueDate)
        {
            taskDescription = _taskDescription;
            dueDate = _dueDate;
        }

        // Standard assigned, non completed task
        public Task(string _memberName, string _taskDescription, DateTime _dueDate)
        {
            memberName = _memberName;
            taskDescription = _taskDescription;
            dueDate = _dueDate;
        }
        #endregion

        #region methods
        // methods
        #endregion
    }
}
