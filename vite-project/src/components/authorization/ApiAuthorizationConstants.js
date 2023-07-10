export const ApplicationName = 'clientApp';

export const QueryParameterNames = {
  ReturnUrl: 'returnUrl',
  Message: 'message'
};

export const LogoutActions = {
  LogoutCallback: 'logout-callback',
  Logout: 'logout',
  LoggedOut: 'logged-out'
};

export const LoginActions = {
  Login: 'login',
  LoginCallback: 'login-callback',
  LoginFailed: 'login-failed',
  Profile: 'profile',
  Register: 'register'
};

const prefix = '/authentication';

export const ApplicationPaths = {
  DefaultLoginRedirectPath: '/',
  ApiAuthorizationClientConfigurationUrl: `_configuration/${ApplicationName}`,
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${LoginActions.Login}`,
  LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
  LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,
  Register: `${prefix}/${LoginActions.Register}`,
  Profile: `${prefix}/${LoginActions.Profile}`,
  LogOut: `${prefix}/${LogoutActions.Logout}`,
  LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
  LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
  IdentityRegisterPath: 'Identity/Account/Register',
  IdentityManagePath: 'Identity/Account/Manage'
};

export const ApplicationSettings = {
  authority: "https://localhost:7191",
  client_id: "clientApp",
  redirect_uri: "https://localhost:5173/authentication/login-callback",
  post_logout_redirect_uri: "https://localhost:5173/authentication/logout-callback",
  response_type: "code",
  scope: "resourceApi openid profile"
};