-- Run the following update queries:
-- Questions

update vts_tbQuestionSelectionMode
set typeassembly = 'SurveyProject.WebControls'
where typeassembly = 'Votations.NSurvey.WebControls'

GO

-- Answers

update vts_tbanswertype
set typeassembly = 'SurveyProject.WebControls'
where typeassembly = 'Votations.NSurvey.WebControls'

GO

-- WebSecurityAddin

update vts_tbWebSecurityAddIn
set typeassembly = 'SurveyProject.WebControls'
where typeassembly = 'Votations.NSurvey.WebControls'


