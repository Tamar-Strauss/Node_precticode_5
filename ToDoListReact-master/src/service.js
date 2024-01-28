import axios from 'axios';
axios.defaults.baseURL = "http://localhost:7271";

const apiUrl = "http://localhost:7271"
var count=0;
export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/Items`)
    return result.data;
  },

  addTask: async (name) => {
    console.log('addTask', name)
    //TODO
    const obj = 
      {"id":count++,"name":name,"isComplate":false}
    const result = await axios.post(`${apiUrl}/Items`, obj, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return {};
  },

  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    const result = await axios.put(`${apiUrl}/Items/${id}`, isComplete, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    //TODO
    return {};
  },

  deleteTask: async (idd) => {
    console.log('deleteTask')
    const result = await axios.delete(`${apiUrl}/Items/${idd}`)
}
}