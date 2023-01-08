using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using WebApi.Domain.Cargo;
using WebApi.Domain.Colaborador;

namespace WebApi.Colaborador.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradorController : ControllerBase
{

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string Exchange = "customers-service";


    public ColaboradorController()
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        _connection = connectionFactory.CreateConnection("colaborador-service-publisher");

        _channel = _connection.CreateModel();

    }


    [HttpPost]
    public async Task<IActionResult> RegisterColaborador(ColaboradorInputModel input)
    {

        try
        {
            var col = new ColaboradorResponseModel();

            col.Nome = input.Nome;
            col.Matricula = input.Matricula.Value;
            col.Cargo = input.Cargo.ToString();
        
            var payload = JsonConvert.SerializeObject(col);
            var byteArray = Encoding.UTF8.GetBytes(payload);
        
            _channel.BasicPublish(Exchange, "customer-created", null, byteArray);
        
            return Ok(col);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            return BadRequest();
        }
        
       

    }


    [HttpGet]
    public async Task<IActionResult> GetAllCargos()
    {

        var values = Enum.GetNames(typeof(Cargo));

        return Ok(values);
    }
    
}