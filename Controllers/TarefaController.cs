using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            Tarefa tarefa = _context.Tarefas.Find(id);
            if (tarefa is null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public ActionResult<List<Tarefa>> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            List<Tarefa> tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

          [HttpGet("ObterPorTitulo")]
        public ActionResult<Tarefa> ObterPorTitulo(string titulo)
        {
            Tarefa tarefa = _context.Tarefas.Where(x => x.Titulo.Equals(titulo)).FirstOrDefault();
            if (tarefa is null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public ActionResult<List<Tarefa>> ObterPorData(DateTime data)
        {
           // var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
           List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
           if (tarefas is null)
           {
            return NotFound();
           }
            return Ok(tarefas);
        }

          [HttpGet("ObterPorStatus")]
        public ActionResult<List<Tarefa>> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            //var tarefa = _context.Tarefas.Where(x => x.Status == status);
            List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();
            if (tarefas is null)
           {
            return NotFound();
           }
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }

             _context.Tarefas.Add(tarefa);
             _context.SaveChanges();

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
           // var tarefaBanco = _context.Tarefas.Find(id);
        Tarefa tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound();
            }
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            }
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            return Ok();
        }

       [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            //var tarefaBanco = _context.Tarefas.Find(id);

             Tarefa tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound();
            }
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            return NoContent();
        }
    }
}
