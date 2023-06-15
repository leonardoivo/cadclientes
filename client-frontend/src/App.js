import React, { useState,useEffect } from 'react'
import ReactDOM from 'react-dom'
import logo from './logo.svg';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal,ModalBody,ModalFooter,ModalHeader } from 'reactstrap';
function App() {


  const baseUrl = "http://127.0.0.1:8080/api/Clientes";

  const[data,setData]=useState([]);


  const [modalIncluir,setModalIncluir] = useState(false);
  const [modalEditar,setModalEditar] = useState(false);
  const [modalExcluir,setModalExcluir] = useState(false);


const [clienteSelecionado, setClienteSelecionado]=useState({
   clienteId:'',
   Nome:'',
   Porte:''
})


const abrirFecharModalIncluir=()=>{setModalIncluir(!modalIncluir)}

const abrirFecharModalEditar=()=>{setModalEditar(!modalEditar)}
const abrirFecharModalExcluir=()=>{setModalExcluir(!modalExcluir)}
const [updateData, setUpdateData] = useState(true);


const handleChange=e=>{
 const {name,value}=e.target;
 setClienteSelecionado({...clienteSelecionado,
  [name]:value});
  console.log(clienteSelecionado)
}

const pedidoGet = async()=>{ 
  await  axios.get(baseUrl)
  .then(response=>{
    setData.data(response.data);
  }).catch(error=>{
    console.log(error);
  })
}

const PedidoPost = async()=>{
    delete clienteSelecionado.clienteId;
    clienteSelecionado.Porte=parseInt(clienteSelecionado.clienteId);
       await axios.post(baseUrl,clienteSelecionado)
    .then(response=>{
      setData(data.concat(response.data));
      abrirFecharModalIncluir();
    }).catch(error=>{
      console.log(error);
    })

}

const PedidoPut = async()=>{
  clienteSelecionado.Porte=parseInt(clienteSelecionado.clienteId);
     await axios.put(baseUrl+"/"+clienteSelecionado.clienteId,clienteSelecionado)
  .then(response=>{
    var resposta=response.data;
    var dadosAuxiliar=data;
    dadosAuxiliar.map(cliente=>{
      if(cliente.clienteId==clienteSelecionado.clienteId){
         cliente.Nome=resposta.Nome;
         cliente.Porte=resposta.Porte;
      }
    });
    abrirFecharModalEditar();
  }).catch(error=>{console.log(error);
  })}

  const PedidoDelete = async()=>{
         await axios.delete(baseUrl+"/"+clienteSelecionado.clienteId)
      .then(response=>{
        setData(data.filter(cliente=>cliente.clienteId !==response.data));
        abrirFecharModalExcluir();        
         }).catch(error=>{
    console.log(error);
  })
  };




const selecionarCliente=(cliente,opcao)=>{
    setClienteSelecionado(cliente);
    (opcao=="Editar") &&
    abrirFecharModalEditar();

}

  useEffect(()=>{
   if(updateData){
    pedidoGet();
    setUpdateData(false);
   }
  },[updateData])

  return (
    <div className="cliente-container">
      <br/>
      <h3>Cadastro de clientes</h3>
      <header className="App-header">
      <button className='btn btn-sucess' onClick={()=>abrirFecharModalIncluir()}>incluir novo cliente</button>

      </header>
      <table className='table table-bordered'>
        <thead>  <tr>
        <td>clienteId</td>
        <td>Nome</td>
        <td>Porte</td>
        <td>Operação</td>

        </tr></thead>
        <tbody>
      {
     data.map(cliente=>(<tr key={cliente.clienteId}>
             <td>cliente.clienteId</td>
        <td>cliente.Nome</td>
        <td>cliente.Porte</td>
        <td>
          <button className='btn btn-primary' onClick={()=>selecionarCliente(cliente,"editar")}>Editar</button>
          <button className='btn btn-danger' onClick={()=>selecionarCliente(cliente,"excluir")}>Excluir</button>

        </td>
        

     </tr>))

      }

        </tbody>
      </table>
      <Modal isOpen={modalIncluir}>
      <ModalHeader>Incluir Clientes</ModalHeader>
       <ModalBody>
        <div className='form-group'>
        <label>clienteId</label>
        {/* <br/> readOnly value={clienteSelecionado && clienteSelecionado.clienteId}/> */}
        <br/>
        <label>Nome</label>
        <br/>
        <input type='text' className='form-control' name='Nome' onChange={handleChange}/>
        <br/>
        <label>Porte</label>
        <br/>
        <input type='text' className='form-control' name='Porte'  onChange={handleChange}/>
        <br/>
       </div></ModalBody>
       <ModalFooter>
       <button className='btn btn-primary' onClick={()=>PedidoPost()}>Incluir</button>
       <button className='btn btn-danger' onClick={()=>abrirFecharModalIncluir()}>Cancelar</button>

       </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditar}>
      <ModalHeader>Editar Clientes</ModalHeader>
       <ModalBody>
        <div className='form-group'>
        <label>clienteId</label>
        {/* <br/> readOnly value={clienteSelecionado && clienteSelecionado.clienteId}/> */}
        <br/>
        <label>Nome</label>
        <br/>
        <input type='text' className='form-control' name='Nome' onChange={handleChange}/>
        <br/>
        <label>Porte</label>
        <br/>
        <input type='text' className='form-control' name='Porte'  onChange={handleChange}/>
        <br/>
       </div></ModalBody>
       <ModalFooter>
       <button className='btn btn-primary' onClick={()=>PedidoPut()}>Editar</button>
       <button className='btn btn-danger' onClick={()=>abrirFecharModalEditar()}>Cancelar</button>

       </ModalFooter>
      </Modal>
      
      <Modal isOpen={modalExcluir}>
      <ModalHeader>Excluir Clientes</ModalHeader>
       <ModalBody>
        Confirma exclusão deste:{clienteSelecionado && clienteSelecionado.clienteId}?
     </ModalBody>
       <ModalFooter>
       <button className='btn btn-primary' onClick={()=>PedidoDelete()}>Editar</button>
       <button className='btn btn-danger' onClick={()=>abrirFecharModalExcluir()}>Cancelar</button>

       </ModalFooter>
      </Modal>

    </div>

  );
}

export default App
