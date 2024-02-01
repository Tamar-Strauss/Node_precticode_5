const sdk = require('api')('@render-api/v1.0#jw0325lr5hblce');

sdk.auth('rnd_GwZfGE0knyrto5CzeKS6mpnbzMl9');
sdk.getServices({limit: '20'})
  .then(({ data }) => console.log(data))
  .catch(err => console.error(err));