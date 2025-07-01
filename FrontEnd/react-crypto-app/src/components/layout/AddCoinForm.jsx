import { useState, useContext, useRef } from "react"
import { Divider, Flex, Select, Space, Typography, Form, Input, 
  InputNumber, Button, DatePicker, Result } from "antd"
import { CryptoContext } from '../../context/crypto-context';
import CoinLabel from "./CoinLabel";


const validateMessages = {
  required: '${label} is required!',
  types:{
    number: '${label} is not valid number'
  },
  number: {
    range: '${label} must be between ${min} and ${max}'
  }
};

export default function AddCoinForm({onClose}){
const [form] = Form.useForm()
const [coin, setCoin] = useState(null)
const {selectCoins, addCoin, user} = useContext(CryptoContext)
const [submitted, setSubmit] = useState(false)
const coinRef = useRef()

const handleSelect = (value) =>{
  console.log(value)
}

if(submitted){
  return (<Result
    status="success"
    title="Successfully added new coin to your wallet!"
    subTitle={`Added ${coinRef.current.amount} of ${coin.name} 
    by price ${coinRef.current.price} to your wallet`}
    extra={[
      <Button type="primary" key="console" onClick={onClose}>
        Close
      </Button>,
    ]}
  />)
}

if(!coin){
    return(
        <Select
    style={{ width: '100%' }}
    onSelect={(v) => setCoin(selectCoins.find((c) => c.symbol === v))}
    value="Select coin"
    options={selectCoins.map((coin)=>({
      label: coin.name,
      value: coin.symbol,
      icon: coin.imageUrl
    }))}
    optionRender={option => (
      <Space>
        <img src={option.data.icon} alt={option.data.label} style={{width: 20}}/> {option.data.label}
      </Space>
    )}
  />
    )
}

const onFinish = (values) =>{
  console.log(values)
  const newCoin = {
    coinId: coin.id,
    symbol: coin.symbol,
    name: coin.name,
    imageUrl: coin.imageUrl,
    amount: values.amount,
    price: values.price,
    transactionDate: values.date?.$d ?? new Date(),
  }
  console.log(newCoin)
  coinRef.current = newCoin
  addCoin(newCoin)
  setSubmit(true)
}

const updateTotal = () => {
  const amount = form.getFieldValue('amount') || 0
  const price = form.getFieldValue('price') || 0
  form.setFieldsValue({
    total: +(amount * price).toFixed(2),
  })
}
console.log(coin.price)
  return(
    <Form
    form={form}
      name="basic"
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 10 }}
      style={{ maxWidth: 600 }}
      initialValues={{
        price: coin.price,
      }}
      onFinish={onFinish}
      validateMessages={validateMessages}>
        <CoinLabel coin={coin}/>
        <Divider/>
    <Form.Item
      label="Amount"
      name="amount"
      rules={[{ 
        required: true, 
        type: 'number',
        min: 0,
      }]}
      style={{ width: '100%', marginRight: '1rem' }}
    >
      <InputNumber onChange={updateTotal} placeholder="Enter coin amount" style={{ width: '100%' }} 
      parser={(value) => value.replace(',', '.').replace(/[^\d.]/g, '')}/>
    </Form.Item>

    <Form.Item
      label="Price"
      name="price"
      rules={[{ 
        required: true, 
        type: 'number',
        min: 0,

        }]}
        style={{ width: '100%'}}
    >
      <InputNumber onChange={updateTotal} placeholder="Enter coin price" style={{ width: '100%' }} step={0.01}/>
    </Form.Item>

    <Form.Item
      label="Date"
      name="date"
        style={{ width: '100%' }}
    >
      <DatePicker showTime style={{ width: '100%' }}/>
    </Form.Item>

    <Form.Item
      label="Total"
      name="total"
    >
      <InputNumber disabled style={{ width: '100%' }}/>
    </Form.Item>


    <Form.Item>
      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form.Item>
  </Form>
    )
}