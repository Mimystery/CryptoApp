import { useState, useContext, useRef } from "react"
import { Divider, Flex, Select, Space, Typography, Form, Input, 
  InputNumber, Button, DatePicker, Result, InputGroup } from "antd"
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


export default function SellCoinForm({onClose, coin}){
const [form] = Form.useForm()
const {selectCoins, addCoin, user, prices} = useContext(CryptoContext)
const [submitted, setSubmit] = useState(false)
const coinRef = useRef()

if(submitted){
  return (<Result
    status="success"
    title="Successfully sold coin from your wallet!"
    subTitle={`Sold ${coinRef.current.amount} of ${coin.name} 
    by price ${coinRef.current.price} from your wallet`}
    extra={[
      <Button type="primary" key="console" onClick={onClose}>
        Close
      </Button>,
    ]}
  />)
}


const onFinish = (values) =>{
  const newCoin = {
    coinId: coin.coinId,
    symbol: coin.symbol,
    name: coin.name,
    imageUrl: coin.imageUrl,
    amount: -(values.amount),
    price: values.price,
    transactionDate: values.date?.$d ?? new Date(),
  }
  console.log(values)
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

  return(
    <Form
    form={form}
      name="basic"
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 10 }}
      style={{ maxWidth: 600 }}
      initialValues={{
        price: +(prices.find(p => p.symbol.replace(/USDT$/, '').toLowerCase() === 
      coin.symbol.toLowerCase())?.price)
      }}
      onFinish={onFinish}
      validateMessages={validateMessages}>
        <CoinLabel coin={coin}/>
        <Divider/>
    <Form.Item
      label="Amount"
      required
      style={{ width: '100%', marginRight: '1rem' }}
    >
    <div style={{ display: 'flex', gap: '8px' }}>
      <Form.Item 
        name="amount"
        noStyle
        rules={[
            {
                required: true,
                type: 'number',
                min: 0,
                validator: (_, value) => {
                    if( value > coin.totalAmount){
                        return Promise.reject(`Max amount is ${coin.totalAmount}`)
                    }
                    if(value<0){
                        return Promise.reject('Amount cannot be negative')
                    }
                    return Promise.resolve()
                },
            },
        ]}>
        <InputNumber 
            min={0}
            step={0.1}
            onChange={(val) => {
                const value = parseFloat(val) || 0;
                if(value > coin.totalAmount){
                    form.setFieldsValue({amount: coin.totalAmount})
                }
                updateTotal()
            }}
            placeholder="Enter coin amount" 
            style={{ width: '100%' }} 
            parser={(value) => value.replace(',', '.').replace(/[^\d.]/g, '')}/>
        </Form.Item>
        <Button onClick={() => {
            const maxValue = parseFloat(coin.totalAmount.toFixed(6))
                form.setFieldsValue({ amount: maxValue})
                updateTotal()
            }}
            type="default">Max
        </Button>
    </div>
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