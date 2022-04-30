import {HTTP} from './http-common';

export const FileApi = {
  getById: function(id) {
    return HTTP.get(`files/${id}`);
  },
  upload: function(formData) {
    return HTTP.post('files', formData);
  }
}
