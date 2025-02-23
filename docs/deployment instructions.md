## Set Up Instructions
### Requirements
- GitHub account
- GitHub actions
- Unity Free/Pro Account
- Ubuntu Server 20.3.26

We are using 2 different repositories for our project:
- https://github.com/KaliKaloo/Kitchen-Feud for our Unity Project
- https://github.com/lokhei/kitchen_feud for the deployment of our game


### CI/CD

#### Workflow
This repository uses actions for automated testing and deployment from the [Game CI project](https://game.ci).

There are two CI/CD workflows present in the `.github/workflows/` folder: `deploy.yml` and
-  `testing.yml`: Used for running the testing suite against new code changes and reports the result to the Discord server, notifying us of any errors.
-  `deploy.yml`: Used to produce a WebGL WASM build which is automatically pushed to our external website for deployment.


#### Adding repository secrets
The following secrets need to be added to this repository in order to run the workfow. Currently, this workflow is set up for Unity Pro account users.

| Key           | Description
| :--           | :----------                                  |
| UNITY_EMAIL   | The email address that you use to login to Unity           |
| UNITY_PASSWORD| The password that you use to login to Unity          |
| UNITY_SERIAL  |  The Serial Key from the [Unity Subscriptions page](https://id.unity.com/en/subscriptions)         |
| GITHUB_TOKEN  | [GitHub Token](https://docs.github.com/en/actions/security-guides/automatic-token-authentication#about-the-github_token-secret) |
| KEY           | The SSH *private* key to deploy to the external repository |
|WEBHOOK_ID     | The Discord Webhook ID                       |
| WEBHOOK_TOKEN | Discord token                                |
| REPOSITORY | external repository in the format username/external-repository         |

In the external repository where the game is hosted, you will need to add the corresponding *public* key as a deploy key.
 
#### Using a Unity Free Account

If using a Unity Free account, the workflow file and Github secrets will need to be [updated](https://game.ci/docs/github/activation) correspondingly.

## Deployment Instructions
### Using GameCI
Simply push to the main branch and the game will automatically deploy using github actions

### Manual Deployment to Github Pages
1. In the Unity Editor, navigate to Edit > Project Settings > Player > Webgl Settings > Resolution and Presentation
2. Enter your preferred canvas width and height, and make sure the WebGL template is set to AgoraTemplate2020
3. In File > Build settings, select WebGL, uncheck development build and build to chosen folder
4. Move index.html, Build/, and TemplateData/ to the root of the git repository which you are using to host the game.
5. Upload all these files to github repository
6. Navigate to your repository > Settings > Pages and set source to master branch 

Your game should now be accessible via https://&lt;user-name>.github.io/&lt;repo-name>
