pipeline
{
	
	agent any

	triggers
	{
		pollSCM("H/5 * * * *")
	}

	stages
	{
		stage("CLEANUP")
		{
			steps
			{
				echo "CLEANUP STARTED"

				dir("Tests")
				{
					sh "rm -rf TestResults/"
				}

				echo "CLEANUP COMPLETED"
			}
		}
		stage("BUILD")
		{
			steps
			{
				echo "BUILD STARTED"
				
				sh "docker build . -t radonapi"
				
				echo "BUILD COMPLETED"
			}
		}
		stage("TEST")
		{
			steps
			{
				echo "TEST STARTED"

				dir("Tests")
				{
					sh "dotnet add package coverlet.collector"
					sh "dotnet test --collect:'XPlat Code Coverage'"
				}
				
				echo "TEST COMPLETED"
			}
			post
			{
				success
				{
					archiveArtifacts "Tests/TestResults/*/coverage.cobertura.xml"
					publishCoverage adapters: [istanbulCoberturaAdapter(path: 'Tests/TestResults/*/coverage.cobertura.xml', thresholds:
					[[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], checksName: '',
						sourceFileResolver: sourceFiles('NEVER_STORE')
				}
			}
		}
		stage("DEPLOY")
		{
			steps
			{
				echo "DEPLOYMENT STARTED"
				
				sh "docker-compose down"
				sh "docker-compose up -d"
				
				archiveArtifacts "Tests/K6Reports/**/*"
				
				echo "DEPLOYMENT COMPLETED"
			}
		}
	}
}
