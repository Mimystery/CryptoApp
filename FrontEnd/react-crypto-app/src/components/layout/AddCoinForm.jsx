import { useState, useContext } from "react"
import { Divider, Flex, Select, Space, Typography, Form, Input } from "antd"
import { CryptoContext } from '../../context/crypto-context';

export default function AddCoinForm(){
const [coin, setCoin] = useState(null)
const {selectCoins} = useContext(CryptoContext)

const handleSelect = (value) =>{
  console.log(value)
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

  return(
    <Form
      name="basic"
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 10 }}
      style={{ maxWidth: 600 }}
      initialValues={{ remember: true }}
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off">
            <Flex align="center">
                <img src={coin.imageUrl} alt={coin.name} style={{width: 40, marginRight: 10}}/>
                <Typography.Title level={2} style={{margin: 0}}>
                    {coin.name}
                </Typography.Title>
            </Flex>
            <Divider/>
    <Form.Item
      label="Username"
      name="username"
      rules={[{ required: true, message: 'Please input your username!' }]}
    >
      <Input />
    </Form.Item>

    <Form.Item
      label="Password"
      name="password"
      rules={[{ required: true, message: 'Please input your password!' }]}
    >
      <Input.Password />
    </Form.Item>

    <Form.Item name="remember" valuePropName="checked" label={null}>
      <Checkbox>Remember me</Checkbox>
    </Form.Item>

    <Form.Item label={null}>
      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form.Item>
  </Form>
    )
}