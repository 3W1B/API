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
					sh "rm -rf K6Reports/"
					sh "mkdir K6Reports"
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
				
				sh "docker run --rm -v /var/lib/jenkins/workspace/RadonAPI/Tests:/Tests -e HOSTING=http://128.140.9.68:5001 loadimpact/k6:latest run /Tests/K6StressTest.js"
				sh "docker run --rm -v /var/lib/jenkins/workspace/RadonAPI/Tests:/Tests -e HOSTING=http://128.140.9.68:5001 loadimpact/k6:latest run /Tests/K6SoakTest.js"
				sh "docker run --rm -v /var/lib/jenkins/workspace/RadonAPI/Tests:/Tests -e HOSTING=http://128.140.9.68:5001 loadimpact/k6:latest run /Tests/K6LoadTest.js"
				
				archiveArtifacts "Tests/K6Reports/**/*"
				
				echo "DEPLOYMENT COMPLETED"
			}
		}
	}
}
