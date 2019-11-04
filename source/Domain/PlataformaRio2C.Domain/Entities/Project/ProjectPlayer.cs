//using System;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ProjectPlayer : Entity
//    {
//        public bool Sent { get; private set; }
//        public int ProjectId { get; private set; }
//        public virtual Project Project { get; private set; }
//        public int PlayerId { get; private set; }
//        public virtual Player Player { get; private set; }
//        public int? SavedUserId { get; private set; }
//        public virtual User SavedUser { get; private set; }
//        public int? SendingUserId { get; private set; }
//        public virtual User SendingUser { get; private set; }
//        public DateTime? DateSaved { get; private set; }
//        public DateTime? DateSending { get; private set; }
//        public int? EvaluationId { get; private set; }
//        public virtual ProjectPlayerEvaluation Evaluation { get; private set; }

//        public int IdNew { get; set; }

//        protected ProjectPlayer()
//        {

//        }

//        public ProjectPlayer(Project project, Player player)
//        {
//            SetProject(project);
//            SetPlayer(player);
//        }

//        public void SetDateSaved(DateTime? dateSaved)
//        {
//            DateSaved = dateSaved;
//        }

//        public void SetDateSending(DateTime? dateSending)
//        {
//            DateSending = dateSending;
//        }

//        public void SetSavedUser(User user)
//        {
//            SavedUser = user;

//            if (Project != null)
//            {
//                SavedUserId = user.Id;
//                DateSaved = DateTime.Now;
//            }
//        }

//        public void SetSendingUser(User user)
//        {
//            SendingUser = user;

//            if (SendingUser != null)
//            {
//                SendingUserId = user.Id;
//                Sent = true;
//                DateSending = DateTime.Now;
//            }
//        }


//        public void SetProject(Project project)
//        {
//            Project = project;

//            if (Project != null)
//            {
//                ProjectId = project.Id;
//            }
//        }

//        public void SetPlayer(Player player)
//        {
//            Player = player;

//            if (Player != null)
//            {
//                PlayerId = player.Id;
//            }
//        }

//        public void SetEvaluation(ProjectPlayerEvaluation evaluation)
//        {
//            Evaluation = evaluation;

//            if (evaluation != null)
//            {
//                EvaluationId = evaluation.Id;
//            }
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
