using System;
using System.Linq;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using System.Data;
using System.Web;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class QuestionExtraLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Localize();
            }

        }
        private void Localize()
        {
            ltPrev.Text = ((PageBase)Page).GetPageResource("Prev");
            ltNext.Text = ((PageBase)Page).GetPageResource("Next");
            ltInsert.Text = ((PageBase)Page).GetPageResource("InsertHyperLink");
            cloneLink.Text = ((PageBase)Page).GetPageResource("CloneButton");

        }
        public void Initialize(int surveyId, int questionId, int displayOrder, int pageNumber)
        {
            string baseURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

            insertLink.HRef = string.Format("{0}/InsertQuestion.aspx?SurveyID={1}&DisplayOrder={2}&Page={3}&MenuIndex={4}&QuestionId={5}",
            UINavigator.AdminRoot, surveyId, displayOrder + 1, pageNumber, -1, questionId);

            var questions = new Questions().GetQuestions(surveyId, null);
            var qEnumeration = questions.Questions.OrderBy(x => x.DisplayOrder);
            var prevQuestion = qEnumeration.LastOrDefault(x => x.DisplayOrder < displayOrder);
            var nextQuestion = qEnumeration.FirstOrDefault(x => x.DisplayOrder > displayOrder);
            cloneLink.CommandArgument = questionId.ToString();

            if (prevQuestion != null)
            {
                string qtype = prevQuestion.SelectionModeId == 4 ? "Matrix" : "Single";
                string qstr = prevQuestion.SelectionModeId == 4 ? "parent" : "";
                prevLink.HRef = string.Format("{0}/Edit{1}Question.aspx?surveyid={2}&{3}questionid={4}&MenuIndex={5}",
                   UINavigator.AdminRoot, qtype, surveyId, qstr, prevQuestion.QuestionId, -1);
            }
            else prevLink.Disabled = true;

            if (nextQuestion != null)
            {
                string qstr = nextQuestion.SelectionModeId == 4 ? "parent" : "";
                string qtype = nextQuestion.SelectionModeId == 4 ? "Matrix" : "Single";
                nextLink.HRef = string.Format("{0}/Edit{1}Question.aspx?surveyid={2}&{3}questionid={4}&MenuIndex={5}",
                UINavigator.AdminRoot,qtype, surveyId, qstr, nextQuestion.QuestionId, -1);
            }
            else nextLink.Disabled = true;
        }


        protected void cloneLink_Click1(object sender, System.EventArgs e)
        {
            int questionId = int.Parse(((LinkButton)sender).CommandArgument);
            var question = new Questions().GetQuestionById(questionId, null);
            var newQuestion = new Question().CloneQuestionById(questionId);

            var q = newQuestion.Questions[0];
            if (q.SelectionModeId == 4)
                UINavigator.NavigateToMatrixQuestionEdit(q.SurveyId, q.QuestionId, -1, -1);
            else
                UINavigator.NavigateToSingleQuestionEdit(q.SurveyId, q.QuestionId, -1, -1);
        }
    }
}
