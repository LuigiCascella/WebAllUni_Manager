namespace ClassLibrary
{
    public class Exam
    {
        public string ExamCode { get; set; }

        public Matter MatterExam { get; set; }

        public string TeacherCode { get; set; }

        public string StudentMatricola { get; set; }

        public DateTime ExamDate { get; set; }

        public int Result { get; set; }

        public Exam() { }

        public Exam(string examCode, Matter matterExam, string teacherCode, string studentMatricola, DateTime examDate, int result)
        {

            ExamCode = examCode;
            MatterExam = matterExam;
            TeacherCode = teacherCode;
            StudentMatricola = studentMatricola;
            ExamDate = examDate;
            Result = result;

        }

    }

}
