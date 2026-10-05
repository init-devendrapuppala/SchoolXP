import { Amplify } from 'aws-amplify';

Amplify.configure({
  Auth: {
    Cognito: {
      userPoolId: 'ap-south-2_vmebmwIXe',
      userPoolClientId: '2kplv2m8c952qnrvf59cep5n1g',
      loginWith: {
        email: true,
      },
    },
  },
});