// import axios from 'axios';
// axios.defaults.baseURL =process.env.REACT_APP_API_URL;
// const apiUrl =process.env.REACT_APP_API_URL;
// var count=0;
// export default {
//   getTasks: async () => {
//     const result = await axios.get(`${apiUrl}/Items`)
//     return result.data;
//   },

//   addTask: async (name) => {
//     console.log('addTask', name)
//     //TODO
//     const obj = 
//       {"id":count++,"name":name,isComplete:false}
//     const result = await axios.post(`${apiUrl}/Items`, obj, {
//       headers: {
//         'Content-Type': 'application/json'
//       }
//     });
//     return {};
//   },

//   setCompleted: async (id, isComplete) => {
//     console.log('setCompleted', { id, isComplete })
//     const result = await axios.put(`${apiUrl}/Items/${id}`, isComplete, {
//       headers: {
//         'Content-Type': 'application/json'
//       }
//     });
//     //TODO
//     return result;
//   },

//   deleteTask: async (idd) => {
//     console.log('deleteTask')
//     const result = await axios.delete(`${apiUrl}/Items/${idd}`)
// }
// }


import axios from 'axios';
axios.defaults.baseURL =process.env.REACT_APP_API_URL;
const apiUrl =process.env.REACT_APP_API_URL;
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
      {id:count++,
       Name:name,
       IsComplate:false}
    const result = await axios.post(`${apiUrl}/Items`, obj, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return {};
  },

    setCompleted: async (id, isComplete) => {
    const task = {
      id: id,
      isComplete: isComplete
    }
    const result = await axios.put(`${apiUrl}/Items/${id}`, task).catch(err => err);
    return result;
  },
  // setCompleted: async (id, isComplete) => {
  //   console.log('setCompleted', { id, isComplete })
  //   const result = await axios.put(`${apiUrl}/Items/${id}`, isComplete, {
  //     headers: {
  //       'Content-Type': 'application/json'
  //     }
  //   });
  //   //TODO
  //   return result.data;
  // },

  deleteTask: async (idd) => {
    console.log('deleteTask')
    const result = await axios.delete(`${apiUrl}/Items/${idd}`)
}
}
