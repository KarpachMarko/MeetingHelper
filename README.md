# Meeting Helper | REST API Backend

dotnet aspnet-codegenerator controller -name QuestionnaireRelationController -actions -m App.Domain.QuestionnaireRelation -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name RequirementUsersController -m App.Domain.RequirementUser -actions -dc AppDbContext -outDir ApiControllers --useDefaultLayout -api --useAsyncActions -f
